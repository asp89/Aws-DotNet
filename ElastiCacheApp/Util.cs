using System.Linq;
using System.Text.RegularExpressions;

namespace ElastiCacheApp
{
    public class Util
    {
        static Regex UpcRegex = new Regex(@"^(\d{8}|\d{12,14})$");
        static int[] UpcWeigths = new int[] { 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1 };

        public static bool IsValid(string code)
        {
            if (!UpcRegex.IsMatch(code))
                return false;

            code = code.PadLeft(14, '0');
            if (code.StartsWith("0000000")) //Used to issue Restricted Ciruculation Numbers within a company
                return false;

            return code
                .Select((x, i) => x - '0')
                .Zip(UpcWeigths, (a, b) => a * b)
                .Sum() % 10 == 0;
        }
    }
}