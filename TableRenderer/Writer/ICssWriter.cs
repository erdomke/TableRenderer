using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Writer
{
  public interface ICssWriter : IDisposable
  {
    void Property(string name);
    void Value(string value);
    void Value(char value);
    void Close();
  }
}
