using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public interface ILinkedImage
  {
    Uri Source { get; }
    string Alt { get; }
  }
}
