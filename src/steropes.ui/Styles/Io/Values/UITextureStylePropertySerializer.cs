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
using System.Xml.Linq;

using Steropes.UI.Platform;
using Steropes.UI.Styles.Io.Parser;

namespace Steropes.UI.Styles.Io.Values
{
  public class UITextureStylePropertySerializer : IStylePropertySerializer
  {
    public UITextureStylePropertySerializer()
    {
    }

    public Type TargetType => typeof(IUITexture);

    public string TypeId => "Texture";

    public object Parse(IStyleSystem styleSystem, XElement reader, IStylePropertyContext context)
    {
      var textureElement = reader.ElementLocal("texture");
      var texture = (string) textureElement?.ElementLocal("name");
      if (string.IsNullOrWhiteSpace(texture))
      {
        texture = null;
      }

      var contentLoader = styleSystem.ContentLoader;
      var tp = (string)reader.ElementLocal("texture-packer") ?? "disabled";
      if (tp == "auto")
      {
        return context.ProcessTexture(contentLoader.LoadTexture(texture));
      }
      return contentLoader.LoadTexture(texture);
    }

    public void Write(IStyleSystem styleSystem, XElement propertyElement, object value)
    {
      var texture = (IUITexture)value;
      propertyElement.Add(new XElement(StyleParser.StyleNamespace + "texture", new XElement(StyleParser.StyleNamespace + "name", texture.Name)));
    }
  }
}