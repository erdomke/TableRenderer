using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableRenderer.Writer;

namespace TableRenderer.Model
{
  public class Column : IElement, ICssRenderable
  {
    public IBackground Background { get; set; }
    public UnitValue Width { get; set; }
    public Visibility Visibility { get; set; }


    public void ToCss(ICssWriter writer)
    {
      if (this.Background != null)
      {
        writer.Property("background");
        this.Background.ToCss(writer);
      }

      if (Visibility != Visibility.Inherit)
      {
        writer.Property("visibility");
        writer.Value(Visibility.ToString().ToLowerInvariant());
      }

      if (Width.Unit != UnitType.Empty)
      {
        writer.Property("width");
        Width.ToCss(writer);
      }
    }
  }
}
