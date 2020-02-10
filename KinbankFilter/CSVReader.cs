using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;

namespace de.ericvogt.KinbankFilter
{
    static class CSVReader
    {
        private const string CSV_EXT = "*.csv";
        private static IEnumerable<string> FilesEnumerable { get; set; }

        /// <summary>
        /// Returns all Files in Folder recursive.
        /// </summary> 
        private static void RecurFileSearch(string filepath)
        {
            FilesEnumerable = Directory.GetFiles(filepath, CSV_EXT, SearchOption.AllDirectories).ToList();
        }

        /// <summary>
        /// Reads Files from Path.
        /// </summary>
        /// <param name="filepath">Read files from path</param>
        public static void ReadFiles(string filepath)
        {
            RecurFileSearch(filepath);
            foreach (var file in FilesEnumerable)
            {
                using (var reader = new StreamReader(file))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        var dataList = line.SplitLine();
                        var entry = LanguageEntry.Instance;
                        var fileName = Path.GetFileNameWithoutExtension(file);
                        entry.GenerateLanguageEntry(fileName, dataList.ToList());
                    }
                }
            }
        }
    }
}
