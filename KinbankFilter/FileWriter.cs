using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using ClosedXML.Excel;

namespace de.ericvogt.KinbankFilter
{
    public class FileWriter
    {
        private readonly string _savePath;
        private readonly string _sheetName;
        private readonly DataTable _dataTable;

        public FileWriter(string savePath, IEnumerable<LanguageEntry> entries, string sheetName = "DATA")
        {
            _savePath = savePath;
            _sheetName = sheetName;
            _dataTable = ConvertToDataTable(entries);
        }

        public bool Save()
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add(_dataTable, _sheetName);
                    workbook.SaveAs(_savePath);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static DataTable ConvertToDataTable(IEnumerable<LanguageEntry> entries)
        {
            var table = new DataTable();
            var properties = TypeDescriptor.GetProperties(typeof(LanguageEntry));

            foreach (PropertyDescriptor prop in properties)
            {
                var columnType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                table.Columns.Add(prop.Name, columnType);
            }

            foreach (var entry in entries)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(entry) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
