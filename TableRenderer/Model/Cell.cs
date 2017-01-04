using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public class Cell : IStyledElement
  {
    private int _columnSpan;
    private int _rowSpan;

    public Style Style { get; set; }
    public int ColumnSpan
    {
      get { return _columnSpan > 1 ? _columnSpan : 1; }
      set { _columnSpan = (value < 1 ? 1 : value); }
    }
    public int RowSpan
    {
      get { return _rowSpan > 1 ? _rowSpan : 1; }
      set { _rowSpan = (value < 1 ? 1 : value); }
    }

    public Cell()
    {
      _columnSpan = 1;
      _rowSpan = 1;
    }
  }
}
