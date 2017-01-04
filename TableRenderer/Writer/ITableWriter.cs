using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableRenderer.Model;

namespace TableRenderer.Writer
{
  public interface ITableWriter : IDisposable
  {
    ITableWriter Configure(IConfiguration config);
    ITableWriter StartPart(IElement elem);
    ITableWriter Value(object value);
    ITableWriter EndPart();
    void Close();
  }
}
