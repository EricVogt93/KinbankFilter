using System.Collections.Generic;
using System.Linq;

namespace de.ericvogt.KinbankFilter
{
    internal static class ExtensionMethods
    {
        private const char Separator = ',';

        public static IEnumerable<string> SplitLine(this string str)
        {
            return str.Split(Separator).ToList();
        }
    }
}
