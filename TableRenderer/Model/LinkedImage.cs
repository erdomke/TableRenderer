using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public class LinkedImage : ILinkedImage
  {
    public string Alt { get; set; }
    public Uri Source { get; set; }
  }
}
