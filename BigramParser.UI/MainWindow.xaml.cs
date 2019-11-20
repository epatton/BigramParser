using BigramParser.Core.Parsers;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
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
            svHistogramResultContainer.Visibility = Visibility.Hidden;

            var header = tblHistogramResult.RowGroups.First().Rows.First();
            tblHistogramResult.RowGroups.First().Rows.Clear();
            tblHistogramResult.RowGroups.First().Rows.Add(header);

            var parser = new BigramTextParser(txtInput.Text);
            var result = parser.Parse().OrderByDescending(x => x.Value);
            if (result.Any())
            {
                foreach (var bigram in result)
                {
                    var row = new TableRow();
                    row.Cells.Add(new TableCell(new Paragraph(new Run(bigram.Key))
                    {
                        BorderThickness = new Thickness(1, 0, 1, 1),
                        BorderBrush = Brushes.DarkGreen
                    }));

                    row.Cells.Add(new TableCell(new Paragraph(new Run(bigram.Value.ToString()))
                    {
                        BorderThickness = new Thickness(0, 0, 1, 1),
                        BorderBrush = Brushes.DarkGreen
                    }));

                    tblHistogramResult.RowGroups.First().Rows.Add(row);
                }

                svHistogramResultContainer.Visibility = Visibility.Visible;
            }
            else
            {
                lblHistogramResultMessage.Content = "Sorry, no bigrams were found in the provided text!";
                lblHistogramResultMessage.Visibility = Visibility.Visible;
            }
        }
    }
}
