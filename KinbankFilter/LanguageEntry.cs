using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.ericvogt.KinbankFilter
{
    public class LanguageEntry
    {
        private static LanguageEntry _instance = null;
        public static LanguageEntry Instance => _instance ?? (_instance = new LanguageEntry());
        public const string SEARCH_ATTR = "parameter";

        public string Name { get; set; }
        public string Parameter { get; set; }
        public string Word { get; set; }
        public string Ipa { get; set; }
        public string Description { get; set; }
        public string Alternative { get; set; }
        public string SrcRaw { get; set; }
        public string SrcBibtex { get; set; }
        public string Comment { get; set; }
        private List<LanguageEntry> EntryList { get; set; }

        private LanguageEntry()
        {
            EntryList = new List<LanguageEntry>();
        }

        /// <summary>
        /// Generates a Language Entry
        /// </summary>
        /// <param name="lang">File- / Languagename</param>
        /// <param name="data">List with data.</param>
        public void GenerateLanguageEntry(string lang, IReadOnlyList<string> data)
        {
            if (data.Count != 8) return;
            var entry = new LanguageEntry()
            {
                Name = lang,
                Parameter = data[0],
                Word = data[1],
                Ipa = data[2],
                Description = data[3],
                Alternative = data[4],
                SrcRaw = data[5],
                SrcBibtex = data[6],
                Comment = data[7]
            };
            EntryList.Add(entry);
        }

        /// <summary>
        /// Returns Entrylist since List should not be visible to other classes.
        /// </summary>
        /// <returns>List with Entries</returns>
        public List<LanguageEntry> GetData()
        {
            return EntryList;
        }

        /// <summary>
        /// Filter Data with a List of Tokens
        /// </summary>
        /// <param name="tokenList">Filter Tokens List</param>
        public void FilterData(List<string> tokenList)
        {
            if (tokenList[0] == "*")
            {
                return;
            }

            var filteredList = (from token in tokenList
                from entry in EntryList
                where !entry.Parameter.Equals(SEARCH_ATTR) & entry.Parameter.Equals(token)
                select entry).ToList();

            EntryList = filteredList;
        }
    }
}