using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClosedXML.Excel;
using de.ericvogt.KinbankFilter;

namespace de.ericvogt.KinbankFilter
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var csvReader = new CSVReader(TB_InputPath.Text);
            csvReader.ReadFiles();

            var filtertokens = TB_Filtertoken.Text.SplitLine();
            LanguageEntry.Instance.FilterData(filtertokens.ToList());
            var dt = LanguageEntry.Instance.FormatToDataTable(LanguageEntry.Instance.GetData());

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "DATA");
            wb.SaveAs(TB_OutputPath.Text);
        }
    }
}
