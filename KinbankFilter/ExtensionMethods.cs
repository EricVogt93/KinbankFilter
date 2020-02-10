using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.ericvogt.KinbankFilter
{
    internal static class ExtensionMethods
    { 
        private const char SEPERATOR = ',';
        public static IEnumerable<string> SplitLine(this string str)
        {
            return str.Split(SEPERATOR).ToList();
        }
    }
}
