using BigramParser.Core.Parsers;
using Microsoft.Win32;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace BigramParser.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var openFileDlg = new OpenFileDialog();
            openFileDlg.DefaultExt = ".txt";
            bool? result = openFileDlg.ShowDialog();

            if (result.HasValue && result.Value == true)
            {
                lblFileName.Content = openFileDlg.FileName;
                txtInput.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
            else
            {
                MessageBox.Show("Something went wrong when opening your file...");
            }
        }

        private void BtnParse_Click(object sender, RoutedEventArgs e)
        {
            lblHistogramResultMessage.Visibility = Visibility.Hidden;
            svResult.Visibility = Visibility.Hidden;
            txtResult.Text = "";
            lblPerformanceTimer.Content = "";

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var parser = new BigramTextParser(txtInput.Text);
            var result = parser.Parse().OrderByDescending(x => x.Value);
            if (result.Any())
            {
                var sb = new StringBuilder();
                foreach (var bigram in result)
                {
                    sb.Append(bigram.Value);
                    sb.Append("\t");
                    sb.Append(bigram.Key.ToString());
                    sb.AppendLine();
                }

                txtResult.Text = sb.ToString();

                sw.Stop();

                svResult.Visibility = Visibility.Visible;

                lblPerformanceTimer.Content = 
                    "Processing Time: " +
                    sw.Elapsed.Minutes + "m " +
                    sw.Elapsed.Seconds + "s " +
                    sw.Elapsed.Milliseconds + "ms";
            }
            else
            {
                lblHistogramResultMessage.Content = "Sorry, no bigrams were found in the provided text!";
                lblHistogramResultMessage.Visibility = Visibility.Visible;
            }

            sw.Stop();
        }
    }
}
