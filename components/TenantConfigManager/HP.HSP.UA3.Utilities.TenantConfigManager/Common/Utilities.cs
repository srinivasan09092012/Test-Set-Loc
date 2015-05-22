using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.HSP.UA3.Utilities.TenantConfigManager.Common
{
    public static class Utilities
    {
        public static string GenerateNewID()
        {
            return Guid.NewGuid().ToString("D").ToUpper();
        }

        public static void StyleXmlEditor(Scintilla editor)
        {
            editor.Lexer = Lexer.Xml;

            editor.StyleClearAll();
            editor.StyleResetDefault();
            editor.Styles[Style.Default].Font = "Consolas";
            editor.Styles[Style.Default].ForeColor = Color.Black;
            editor.Styles[Style.Default].Size = 10;

            editor.Styles[Style.Xml.Default].ForeColor = Color.Black;
            editor.Styles[Style.Xml.Comment].ForeColor = Color.Green;
            editor.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
            editor.Styles[Style.Xml.AttributeUnknown].ForeColor = Color.Blue;
            editor.Styles[Style.Xml.SingleString].ForeColor = Color.Black;
            editor.Styles[Style.Xml.CData].ForeColor = Color.Blue;

            editor.Styles[Style.Xml.DoubleString].ForeColor = Color.Blue;

            editor.Styles[Style.Xml.Tag].ForeColor = Color.Maroon;
            editor.Styles[Style.Xml.TagEnd].ForeColor = Color.Maroon;

            editor.Styles[Style.Xml.XmlStart].ForeColor = Color.Black;
            editor.Styles[Style.Xml.XmlEnd].ForeColor = Color.Black;
        }
    }
}
