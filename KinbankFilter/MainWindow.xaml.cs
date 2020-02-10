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
using System.IO;
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

        private string _savePath;
        private string _loadPath;
        private IEnumerable<string> _filtertokens;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Filter Button Click Event
        /// </summary>
        /// <param name="sender">Filter Button</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _savePath = TB_OutputPath.Text;
            _loadPath = TB_InputPath.Text;
            _filtertokens = TB_Filtertoken.Text.SplitLine().ToList();
            if (_loadPath == string.Empty | !_filtertokens.Any() | _savePath == string.Empty) 
                return;
            
            CSVReader.ReadFiles(_loadPath);
            LanguageEntry.Instance.FilterData(_filtertokens.ToList());

            var writer = new FileWriter(_savePath, LanguageEntry.Instance.GetData());
            writer.Save();
            Response();
        }

        /// <summary>
        /// Visible Response if Save was successful.
        /// </summary>"
        /// <returns>True or False</returns>
        private void Response()
        {
            var checkMark = new Uri(@"/_assets/checkmark.png", UriKind.Relative);
            var cross = new Uri(@"/_assets/cross.png", UriKind.Relative);

            ImageResponse.Source = File.Exists(_savePath) ? new BitmapImage(checkMark) : new BitmapImage(cross);
        }   
    }
}
