using System.Collections.Generic;
using System.Linq;

namespace de.ericvogt.KinbankFilter
{
    public class LanguageEntry
    {
        public string Name { get; set; }
        public string Parameter { get; set; }
        public string Word { get; set; }
        public string Ipa { get; set; }
        public string Description { get; set; }
        public string Alternative { get; set; }
        public string SrcRaw { get; set; }
        public string SrcBibtex { get; set; }
        public string Comment { get; set; }
    }

    public class LanguageEntryService
    {
        private const string HeaderParameter = "parameter";
        private const string WildcardToken = "*";

        private List<LanguageEntry> _entries = new List<LanguageEntry>();

        public void Clear()
        {
            _entries.Clear();
        }

        public void AddEntry(string languageName, IReadOnlyList<string> data)
        {
            if (data.Count != 8)
                return;

            var entry = new LanguageEntry
            {
                Name = languageName,
                Parameter = data[0],
                Word = data[1],
                Ipa = data[2],
                Description = data[3],
                Alternative = data[4],
                SrcRaw = data[5],
                SrcBibtex = data[6],
                Comment = data[7]
            };

            _entries.Add(entry);
        }

        public List<LanguageEntry> GetEntries()
        {
            return _entries;
        }

        public void FilterByTokens(IReadOnlyList<string> tokens)
        {
            if (tokens == null || tokens.Count == 0)
                return;

            if (tokens[0] == WildcardToken)
                return;

            _entries = _entries
                .Where(e => !e.Parameter.Equals(HeaderParameter) && tokens.Contains(e.Parameter))
                .ToList();
        }
    }
}
