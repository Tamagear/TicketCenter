using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketautomat.Classes
{
    public static class StringHelpers
    {
        public static List<string> XML_Get(string input, string separatorTag)
        {
            return new List<string>(input.Split(new string[] { $"<{separatorTag}>", $"</{separatorTag}>" }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static string XML_GetSingle(string input, string separatorTag)
        {
            string[] s = input.Split(new string[] { $"<{separatorTag}>", $"</{separatorTag}>" }, StringSplitOptions.RemoveEmptyEntries);
            return s.Length > 1 ? s[1] : s != null ? s[0] : string.Empty;
        }
    }
}
