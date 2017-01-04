using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableRenderer.Model;

namespace TableRenderer.Writer
{
  public class ElementStack : IEnumerable<IElement>
  {
    private Stack<IElement> _elems = new Stack<IElement>();
    private Stack<Style> _styles = new Stack<Style>();

    public ElementStack() { }
    public ElementStack(Style style)
    {
      _styles.Push(style);
    }

    public Style CurrentStyle
    {
      get
      {
        if (_styles.Count > 0)
          return _styles.Peek() ?? new Style();
        return new Style();
      }
    }

    public void Push(IElement elem)
    {
      _elems.Push(elem);
      var styled = elem as IStyledElement;
      if (styled != null)
      {
        if (_styles.Count > 0)
        {
          _styles.Push(Style.GetMerged(_styles.Peek(), styled.Style));
        }
        else
        {
          _styles.Push(styled.Style);
        }
      }
    }
    public IElement Pop()
    {
      var popped = _elems.Pop();
      if (popped is IStyledElement)
        _styles.Pop();
      return popped;
    }

    public IEnumerator<IElement> GetEnumerator()
    {
      return _elems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _elems.GetEnumerator();
    }

  }
}
