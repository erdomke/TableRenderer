using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public struct UnitContext
  {
    public double PercentageBasis { get; set; }
    public double FontHeight { get; set; }
    public double RootFontHeight { get; set; }
    public double ViewportHeight { get; set; }
    public double ViewportWidth { get; set; }
  }
}
