using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableRenderer.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TableRenderer.Writer.Tests
{
  [TestClass()]
  public class HtmlWriterTests
  {
    /* [TestMethod()]
     public void DataTypes_Html()
     {
       using (var text = new StringWriter())
       using (var writer = new HtmlWriter(text))
       {
         TableSample.DataTypes(writer);
         writer.Close();
         Assert.AreEqual("<table xmlns:x=\"urn:schemas-microsoft-com:office:excel\">\r\n  <thead>\r\n    <tr>\r\n      <th style=\"width: 200px\" x:str=\"\">String</th>\r\n      <th style=\"width: 200px\" x:str=\"\">Date</th>\r\n      <th style=\"width: 200px\" x:str=\"\">Number</th>\r\n      <th style=\"width: 20px\" x:str=\"\">Other</th>\r\n    </tr>\r\n  </thead>\r\n  <tbody>\r\n    <tr>\r\n      <td x:str=\"\">a thing</td>\r\n      <td>\r\n        <time datetime=\"2000-11-04T02:30:20\">11/4/2000 2:30:20 PM</time>\r\n      </td>\r\n      <td x:num=\"52.3\">52.3</td>\r\n      <td>\r\n        <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAZdEVYdFNvZnR3YXJlAEFkb2JlIEltYWdlUmVhZHlxyWU8AAABxElEQVQ4T6WTb2tSYRjGn++yj9BXGL1sLWbgig2D1XSZMbONUGkMq6FkqVBMbG6yxtqZnCmSJexPjWNoUr0LtBK3YdGLaunGenP1XE9/8IWBa8KP5+a6r985h3NQADgSbcPDIJxRsxiJnNKsoZMYvtvTEezSoSsc90zaQ2MKL+s6CjvJjmCXDl15gT40f9TxbW9L8bVJav/gV4fQoStGo6fRONjBgL8blsBxXH3QD1esX51jM2cUrRk77NKhK1wxM77sf8Dl6T545gbwvDyDZ5L42nVEsl4E0+NqZsYdO86oSTl0xbX4WXxsvoXv0QUY1QVsvp9XuGctMLZSCvec5W/ODrt06ApvYhDbu6/hX76IF7Ul2CO96ryl2WHUdAXn1h27dOiKiflzqO6WEM44UaqnUdiWX0MypY0gL+9OOP/J2QlnriiHrrixOITK94J8zEEMh0/g1ecsSp8eI/XmPhKbNxWcmXFnDfXIF2vGO+nQFf5lG8qNIjL5ABZWJ5ErhvGkEMLTYkjNhDMzzvGcB9qGDxXp+JM2CF/i/HpQtyO4ckniUOdtXcKzFZlxd+d3hw5dIX9dkmP/SVfbP8hhaBt2DsRPktbEH/fmuSwAAAAASUVORK5CYII=\" />\r\n      </td>\r\n    </tr>\r\n    <tr>\r\n      <td x:str=\"\">a second thing</td>\r\n      <td>\r\n        <time datetime=\"2000-10-03\">10/3/2000 12:00:00 AM</time>\r\n      </td>\r\n      <td x:num=\"15\">15</td>\r\n      <td>\r\n        <a href=\"http://www.google.com/\">http://www.google.com/</a>\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>"
           , text.ToString());
       }
     }

     [TestMethod()]
     public void MergedCells_Html()
     {
       using (var text = new StringWriter())
       using (var writer = new HtmlWriter(text))
       {
         TableSample.MergedCells(writer);
         writer.Close();
         Assert.AreEqual("<table xmlns:x=\"urn:schemas-microsoft-com:office:excel\">\r\n  <tbody>\r\n    <tr>\r\n      <td rowspan=\"2\" x:str=\"\">First</td>\r\n      <td colspan=\"2\" x:str=\"\">Second</td>\r\n      <td colspan=\"2\" rowspan=\"2\" x:str=\"\">Third</td>\r\n    </tr>\r\n    <tr>\r\n      <td colspan=\"2\" x:str=\"\">Fourth</td>\r\n    </tr>\r\n  </tbody>\r\n</table>"
           , text.ToString());
       }
     }*/
  }
}
