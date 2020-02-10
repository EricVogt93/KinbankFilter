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
    class LanguageEntry
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
            var filteredList = (from token in tokenList
                from entry in EntryList
                where !entry.Parameter.Equals(SEARCH_ATTR) & entry.Parameter.Equals(token)
                select entry).ToList();

            EntryList = filteredList;
        }

        public DataTable FormatToDataTable<T>(IList<T> data)
        {
            var table = new DataTable();

            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {

                var dc = new DataColumn("Value");
                table.Columns.Add(dc);
                foreach (T item in data)
                {
                    DataRow dr = table.NewRow();
                    dr[0] = item;
                    table.Rows.Add(dr);
                }
            }
            else
            {
                var properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                foreach (var item in data)
                {
                    var row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        try
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        catch (Exception ex)
                        {
                            row[prop.Name] = DBNull.Value;
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }
}