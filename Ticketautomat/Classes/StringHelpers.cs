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

        public static bool XML_ContainsTag(string input, string separatorTag)
        {
            return input.Contains($"<{separatorTag}>") && input.Contains($"</{separatorTag}>");
        }

        public static bool XML_IsValid(string input, string separatorTag)
        {
            return input.StartsWith($"<{separatorTag}>");
        }

        public static string XML_GetSingle2(string input, string separatorTag)
        {
            int firstPosition = input.IndexOf($"<{separatorTag}>");
            int secondPosition = input.IndexOf($"</{separatorTag}>");
            string output = input.Substring(firstPosition + $"<{separatorTag}>".Length, secondPosition - firstPosition - $"<{separatorTag}>".Length);
            return output;
        }
    }
}
