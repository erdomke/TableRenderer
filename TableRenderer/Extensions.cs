using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableRenderer.Model;
using TableRenderer.Writer;

namespace TableRenderer
{
  public static class Extensions
  {
    public static ITableWriter FullPart(this ITableWriter writer, IElement elem)
    {
      return writer.StartPart(elem).EndPart();
    }
  }
}
