﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Steropes.UI.Annotations;

namespace Steropes.UI.Bindings
{
  internal class PropertyMemberBinding : IReadOnlyObservableValue
  {
    readonly IReadOnlyObservableValue eventSource;
    readonly Func<object, object> propertyAccess;
    readonly string propertyName;
    object source;
    object value;
    bool alreadyHandlingChange;

    PropertyMemberBinding(IReadOnlyObservableValue source)
    {
      eventSource = source ?? throw new ArgumentNullException(nameof(source));

      if (eventSource != null)
      {
        eventSource.PropertyChanged += OnBindingValueChanged;
      }

      Source = source.Value;
    }

    public void Dispose()
    {
      this.eventSource.PropertyChanged -= OnBindingValueChanged;
      Source = null;
    }

    public IReadOnlyList<IBindingSubscription> Sources => new[] { eventSource };

    void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == propertyName)
      {
        Value = propertyAccess(Source);
      }
    }

    public PropertyMemberBinding(IReadOnlyObservableValue source, PropertyInfo propertyInfo) : this(source)
    {
      if (propertyInfo == null)
      {
        throw new ArgumentNullException();
      }

      this.propertyName = propertyInfo.Name;
      propertyAccess = CreatePropertyAccess(propertyInfo);
      Value = propertyAccess(Source);
    }

    public PropertyMemberBinding(IReadOnlyObservableValue source, FieldInfo propertyInfo) : this(source)
    {
      if (propertyInfo == null)
      {
        throw new ArgumentNullException();
      }

      this.propertyName = propertyInfo.Name;
      propertyAccess = CreateFieldAccess(propertyInfo);
      Value = propertyAccess(Source);
    }

    public object Value
    {
      get { return value; }
      set
      {
        if (Equals(value, this.value)) return;
        this.value = value;
        OnPropertyChanged();
      }
    }

    public object Source
    {
      get { return source; }
      set
      {
        if (Equals(source, value))
        {
          return;
        }

        if (source is INotifyPropertyChanged onc)
        {
          onc.PropertyChanged -= OnSourcePropertyChanged;
        }

        source = value;
        if (source is INotifyPropertyChanged nnc)
        {
          nnc.PropertyChanged += OnSourcePropertyChanged;
        }

        Value = value != null ? propertyAccess?.Invoke(value) : null;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    Func<object, object> CreatePropertyAccess(PropertyInfo propertyInfo)
    {
      var t = propertyInfo.ReflectedType ?? throw new ArgumentNullException();
      var param = Expression.Parameter(typeof(object));

      // object param;
      // pp = (object) ((T) param).Property;
      var cast = t.IsValueType ? Expression.TypeAs(param, t) : Expression.Convert(param, t);

      var getter = Expression.Property(cast, propertyInfo);
      var result = Expression.Convert(getter, typeof(object));
      var expression = Expression.Lambda<Func<object, object>>(result, param);
      return expression.Compile();
    }

    Func<object, object> CreateFieldAccess(FieldInfo propertyInfo)
    {
      var t = propertyInfo.ReflectedType ?? throw new ArgumentNullException();
      var param = Expression.Parameter(typeof(object));

      // object param;
      // pp = (object) ((T) param).Property;
      var cast = t.IsValueType ? Expression.TypeAs(param, t) : Expression.Convert(param, t);

      var getter = Expression.Field(cast, propertyInfo);
      var result = Expression.Convert(getter, typeof(object));
      var expression = Expression.Lambda<Func<object, object>>(result, param);
      return expression.Compile();
    }

    void OnBindingValueChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(eventSource.Value))
      {
        Source = eventSource.Value;
      }
    }

    [NotifyPropertyChangedInvocator]
    void OnPropertyChanged([CallerMemberName] string propertyName1 = null)
    {
      if (!alreadyHandlingChange)
      {
        try
        {
          alreadyHandlingChange = true;
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName1));
        }
        finally
        {
          alreadyHandlingChange = false;
        }
      }
    }
  }
}