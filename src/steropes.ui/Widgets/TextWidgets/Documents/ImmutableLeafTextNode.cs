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

namespace Steropes.UI.Widgets.TextWidgets.Documents
{
  public class ImmutableLeafTextNode : ITextNode
  {
    readonly ITextPosition endOffset;

    readonly ITextPosition offset;

    public ImmutableLeafTextNode(ITextDocument document, ITextPosition offset, ITextPosition endOffset)
    {
      Document = document;
      this.offset = offset;
      this.endOffset = endOffset;
    }

    public int Count => 0;

    public ITextDocument Document { get; }

    public int EndOffset => endOffset.Offset;

    public bool Leaf => true;

    public int Offset => offset.Offset;

    public ITextNode this[int index]
    {
      get
      {
        throw new IndexOutOfRangeException();
      }
    }

    public ITextNode Replace(int operationOffset, int removedChildrenCount, ITextNode[] addedChildren)
    {
      throw new InvalidOperationException("Leaf-Nodes do not support child-nodes.");
    }
  }
}