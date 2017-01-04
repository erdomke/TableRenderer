using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public class FontSet
  {
    public string Cursive { get; set; }
    public string Fantasy { get; set; }
    public string Monospace { get; set; }
    public string SansSerif { get; set; }
    public string Serif { get; set; }

    private static FontSet _msOffice = new FontSet()
    {
      Cursive = "Cambria",
      Fantasy = "Cambria",
      Monospace = "Consolas",
      SansSerif = "Calibri",
      Serif = "Cambria"
    };
    public static FontSet MsOffice { get { return _msOffice; } }
    private static FontSet _standard = new FontSet()
    {
      Cursive = "Times New Roman",
      Fantasy = "Times New Roman",
      Monospace = "Courier New",
      SansSerif = "Arial",
      Serif = "Times New Roman"
    };
    public static FontSet Standard { get { return _standard; } }
  }
}
