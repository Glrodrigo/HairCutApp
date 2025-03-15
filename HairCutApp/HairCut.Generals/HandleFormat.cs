using System.Text;
using System.Text.RegularExpressions;

namespace HairCut.Generals
{
    public class HandleFormat
    {
        public static string CleanName(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
                return string.Empty;

            accountName = Regex.Replace(accountName, @"[^a-zA-Z\s]", "");

            accountName = Regex.Replace(accountName, @"\s+", " ");

            return accountName.Trim();
        }

        public static string OnlyNumbers(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            var subString = new StringBuilder(str);
            var firstAux = 0;
            var secondAux = 0;

            while (secondAux < subString.Length)
            {
                var isDigit = char.IsDigit(subString[secondAux]);
                if (isDigit)
                {
                    subString[firstAux++] = subString[secondAux++];
                }
                else
                {
                    ++secondAux;
                }
            }
            subString.Length = firstAux;

            var numbers = subString.ToString();
            return numbers;
        }

        public static string StringCodeFormat(string str, int isNull = 0)
        {
            str = HandleFormat.OnlyNumbers(str);

            int padLeft = str.Length - str.TrimStart('0').Length;

            if (!long.TryParse(str, out var aux))
            {
                aux = isNull;
                return aux.ToString();
            }

            string result = aux.ToString().PadLeft(padLeft + aux.ToString().Length, '0');

            return result;
        }
    }
}
