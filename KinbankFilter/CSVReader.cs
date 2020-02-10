using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;

namespace de.ericvogt.KinbankFilter
{
    class CSVReader
    {
        private string FilePath { get; set; }
        private const string CSV_EXT = "*.csv";
        private IEnumerable<string> FilesEnumerable { get; set; }
        

        public CSVReader(string filepath)
        {
            FilePath = filepath;
            RecurFileSearch();
        }

        /// <summary>
        /// Returns all Files in Folder recursive.
        /// </summary> 
        private void RecurFileSearch()
        {
            FilesEnumerable = Directory.GetFiles(FilePath, CSV_EXT, SearchOption.AllDirectories).ToList();
        }

        public void ReadFiles()
        {
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
