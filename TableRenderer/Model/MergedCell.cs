using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public class MergedCell : Cell
  {
    private List<object> _cache;

    internal IList<object> Cache { get { return _cache; } }
    internal int StartCol { get; set; }
    internal int StartRow { get; set; }

    internal CellAddress GetStartAddress()
    {
      return new CellAddress(StartRow, StartCol);
    }
    internal CellAddress GetEndAddress()
    {
      return new CellAddress(StartRow + RowSpan - 1, StartCol + ColumnSpan - 1);
    }

    internal MergedCell(Cell orig)
    {
      ColumnSpan = orig.ColumnSpan;
      RowSpan = orig.RowSpan;
      Style = orig.Style;

      _cache = new List<object>();
      _cache.Add(this);
    }

    internal bool Applies(int rowIndex, int colIndex)
    {
      return colIndex >= StartCol
        && colIndex < StartCol + ColumnSpan
        && rowIndex >= StartRow
        && rowIndex < StartRow + RowSpan;
    }
  }
}
