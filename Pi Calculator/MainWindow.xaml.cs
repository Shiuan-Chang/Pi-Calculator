using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pi_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<int, result> results = new Dictionary<int, result>();

        public MainWindow()
        {
            InitializeComponent();
            dataTable.ItemsSource = new List<result>();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(number.Text, out int sampleSize)) 
            {
                if (!results.ContainsKey(sampleSize))
                {
                    var stopWatech = Stopwatch.StartNew();
                    double pi = Calculator.calculate(sampleSize);
                    stopWatech.Stop();

                    var data = new result
                    {
                        sample = sampleSize,
                        time = stopWatech.Elapsed,
                        value = pi
                    };

                    results[sampleSize] = data;

                    dataTable.ItemsSource = null;
                    dataTable.ItemsSource = results.Values.ToList();
                }

                else if (results.ContainsKey(sampleSize))
                {
                    MessageBox.Show("資料已經存在");
                }

                else 
                {
                    MessageBox.Show("請輸入有效數值");
                }
            
            
            }
        }
    }
}