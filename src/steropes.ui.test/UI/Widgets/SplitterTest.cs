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
using FluentAssertions;

using NUnit.Framework;

using Steropes.UI.Components;
using Steropes.UI.Widgets.Container;

namespace Steropes.UI.Test.UI.Widgets
{
  [Category("Container Widgets")]
  public class SplitterTest
  {
    [Test]
    public void MeasureInfinite()
    {
      var s = new Splitter(LayoutTestStyle.Create(), Direction.Left) { FirstPane = LayoutTestWidget.FixedSize(500, 500), SecondPane = LayoutTestWidget.FixedSize(200, 200) };

      s.UIStyle.StyleResolver.AddRoot(s);
      s.Measure(Size.Auto);

      s.DesiredSize.Should().Be(new Size(310, 500));
    }

    // todo: Test splitter component sizes
  }
}