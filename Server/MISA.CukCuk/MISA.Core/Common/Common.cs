using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Common
{
    public static class Common
    {
        public static string FormatDate(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            string[] parts = input.Split("/");
            if (parts.Length == 0) return "";
            else
            {
                string formated = "";
                if(parts.Length == 1)
                {
                    formated = "01/01/" + input;
                }
                if(parts.Length == 2)
                {
                    formated = "01/" + input;
                }
 
                string pattern = @"(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)";
                MatchCollection matched = Regex.Matches(input, pattern);
                foreach (Match match in matched)
                {
                    return match.Value;
                }
            }
            return "";
        }
    }
}
