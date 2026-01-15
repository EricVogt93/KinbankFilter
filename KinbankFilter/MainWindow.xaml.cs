using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace de.ericvogt.KinbankFilter
{
    public partial class MainWindow : Window
    {
        private readonly LanguageEntryService _languageService = new LanguageEntryService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var inputPath = TB_InputPath.Text;
            var outputPath = TB_OutputPath.Text;
            var filterTokens = TB_Filtertoken.Text.SplitLine().ToList();

            if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath) || !filterTokens.Any())
                return;

            _languageService.Clear();
            CsvReader.ReadFiles(inputPath, _languageService);
            _languageService.FilterByTokens(filterTokens);

            var writer = new FileWriter(outputPath, _languageService.GetEntries());
            var success = writer.Save();

            ShowResponse(success);
        }

        private void ShowResponse(bool success)
        {
            var imageName = success ? "checkmark.png" : "cross.png";
            var uri = new Uri($"pack://application:,,,/KinbankFilter;component/_assets/{imageName}");
            ImageResponse.Source = new BitmapImage(uri);
        }
    }
}
