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

using Microsoft.Xna.Framework;
using Steropes.UI.Platform;
using Steropes.UI.Styles;

namespace Steropes.UI.Widgets.Styles
{
  public class ImageStyleDefinition : IStyleDefinition
  {
    public IStyleKey<IUITexture> Texture { get; private set; }

    public IStyleKey<ScaleMode> TextureScale { get; private set; }
    
    public IStyleKey<Color> TextureColor { get; private set; }

    public void Init(IStyleSystem s)
    {
      Texture = s.CreateKey<IUITexture>("texture", false);
      TextureScale = s.CreateKey<ScaleMode>("texture-scale", false);
      TextureColor = s.CreateKey<Color>("texture-color", false);
    }
  }
}