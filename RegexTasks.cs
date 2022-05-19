using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegTasks
{
    static public class RegexTasks
    {
        static public string Task2(string text) //задача 2
        {
            string result = string.Empty;
            Regex regex1 = new Regex(@"dac""*|da*c|dc"),
                  regex2 = new Regex(@"da*c");

            var matches1 = regex1.Matches(text);
            var matches2 = regex2.Matches(text);

            result += "1 выражение:\n";

            foreach (Match match in matches1)
            {
                result += $"{match.Value} c {match.Index} по {match.Index + match.Value.Length}\n";
            }

            result += "\n2 выражение:\n";

            foreach (Match match in matches2)
            {
                result += $"{match.Value} c {match.Index} по {match.Index + match.Value.Length}\n";
            }

            return result;
        }
        static public string Task1(string text)//задача 1
        {
            string result = string.Empty;
            Regex regex1 = new Regex(@"aa*bb*c"),
                  regex2 = new Regex(@"012(12)*3*");

            var matches1 = regex1.Matches(text);
            var matches2 = regex2.Matches(text);

            result += "1 выражение:\n";

            foreach (Match match in matches1)
            {
                result += $"{match.Value} c {match.Index} по {match.Index + match.Value.Length}\n";
            }

            result += "\n2 выражение:\n";

            foreach (Match match in matches2)
            {
                result += $"{match.Value} c {match.Index} по {match.Index + match.Value.Length}\n";
            }

            return result;
        }
    }

}
