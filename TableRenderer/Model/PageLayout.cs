using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model
{
  public class PageLayout : IConfiguration
  {
    public UnitValue FooterHeight { get; set; }
    public UnitValue HeaderHeight { get; set; }
    public UnitValue Height { get; set; }
    public Padding Margin { get; set; }
    public UnitValue Width { get; set; }
  }
}
