using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Data;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClosedXML.Excel;
using de.ericvogt.KinbankFilter;

namespace de.ericvogt.KinbankFilter
{
    public class FileWriter
    {
        private string _savepath;
        private string _sheetname;
        private DataTable _dataTable;
        public FileWriter(string savepath, IEnumerable<LanguageEntry> dataEnumerable, string sheetname= "DATA")
        {
            _savepath = savepath;
            _sheetname = sheetname;
            _dataTable = new DataTable();
            _dataTable = FormatToDataTable(dataEnumerable);
        }

        /// <summary>
        /// Formatting an Enumerable into a Datatable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>Datatable</returns>
        private DataTable FormatToDataTable(IEnumerable data)
        {
            var table = new DataTable();

            var properties = TypeDescriptor.GetProperties(typeof(LanguageEntry));
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
                    catch (Exception)
                    {
                        row[prop.Name] = DBNull.Value;
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// Saves the Data.
        /// </summary>
        public void Save()
        {
            try
            {
                var wb = new XLWorkbook();
                wb.Worksheets.Add(_dataTable, _sheetname);
                wb.SaveAs(_savepath);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
