﻿// MIT License
// Copyright (c) 2011-2016 Elisée Maurer, Sparklin Labs, Creative Patterns
// Copyright (c) 2016 Thomas Morgner, Rabbit-StewDio Ltd.
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System;
using System.Linq.Expressions;

using Steropes.UI.Util;

namespace Steropes.UI.Test
{
  public class ExpressionCache
  {
    readonly LRUCache<Tuple<Type, string>, Func<object, object>> cachedAccessors;

    public ExpressionCache()
    {
      cachedAccessors = new LRUCache<Tuple<Type, string>, Func<object, object>>(50);
    }

    public Func<object, object> Get(Type t, string property)
    {
      var key = Tuple.Create(t, property);
      if (cachedAccessors.TryGetValue(key, out var fn))
      {
        return fn;
      }

      try
      {
        fn = CreatePropertyAccess(t, property);
      }
      catch
      {
        fn = Null;
      }

      cachedAccessors.Add(key, fn);
      return fn;
    }

    Func<object, object> CreatePropertyAccess(Type t, string name)
    {
      var param = Expression.Parameter(typeof(object));

      // object param;
      // pp = (object) ((T) param).Property;
      var pp = Expression.Convert(Expression.Property(Expression.Convert(param, t), t.GetProperty(name)), typeof(object));
      var expression = Expression.Lambda<Func<object, object>>(pp, param);
      return expression.Compile();
    }

    object Null(object param)
    {
      return null;
    }
  }
}