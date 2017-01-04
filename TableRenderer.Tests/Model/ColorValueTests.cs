using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableRenderer.Model.Tests
{
  [TestClass()]
  public class ColorValueTests
  {
    private void AssertRed(ColorValue value)
    {
      Assert.AreEqual(255, value.R);
      Assert.AreEqual(0, value.G);
      Assert.AreEqual(51, value.B);
      Assert.AreEqual(255, value.A);
    }

    [TestMethod()]
    public void VerifyColorValueParse()
    {
      AssertRed((ColorValue)"#f03");
      AssertRed((ColorValue)"#f03f");
      AssertRed((ColorValue)"#F03");
      AssertRed((ColorValue)"#F03F");
      AssertRed((ColorValue)"#ff0033");
      AssertRed((ColorValue)"#ff0033ff");
      AssertRed((ColorValue)"#FF0033");
      AssertRed((ColorValue)"#FF0033FF");
      AssertRed((ColorValue)"rgb(255,0,51)");
      AssertRed((ColorValue)"rgb(255, 0, 51)");
      AssertRed((ColorValue)"rgba(255,0,51,1)");
      AssertRed((ColorValue)"rgba(255, 0, 51,1)");
      AssertRed((ColorValue)"rgb(100%,0%,20%)");
      AssertRed((ColorValue)"rgb(100%, 0%, 20%)");
      Assert.AreEqual("#F03", (string)((ColorValue)"rgb(255,0,51)"));
    }
  }
}
