using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public class Table : IStyledElement
  {
    public bool AutoFilter { get; set; }
    public Style Style { get; set; }
    public int RepeatRowsEachPage { get; set; }
  }
}
