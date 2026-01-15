using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace de.ericvogt.KinbankFilter
{
    public static class CsvReader
    {
        private const string CsvExtension = "*.csv";

        public static void ReadFiles(string directoryPath, LanguageEntryService service)
        {
            var files = GetCsvFiles(directoryPath);

            foreach (var file in files)
            {
                ReadFile(file, service);
            }
        }

        private static IEnumerable<string> GetCsvFiles(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, CsvExtension, SearchOption.AllDirectories);
        }

        private static void ReadFile(string filePath, LanguageEntryService service)
        {
            var languageName = Path.GetFileNameWithoutExtension(filePath);

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = line.SplitLine().ToList();
                    service.AddEntry(languageName, data);
                }
            }
        }
    }
}
