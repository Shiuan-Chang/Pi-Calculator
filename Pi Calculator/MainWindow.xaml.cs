using System.Collections.ObjectModel;
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
using Pi_Calculator.Models;
using Pi_Calculator.Presenters;
using Pi_Calculator.Utilities;
using static Pi_Calculator.Contract.PIMissionContract;

namespace Pi_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// MVVM做前端，MVP做後端

    public partial class MainWindow : Window, IPIMissionView
    {
        private MainViewPresenter mainViewPresenter;
        public MainViewModel viewModel { get; set; } = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            mainViewPresenter = new MainViewPresenter(this);
            DataContext = viewModel;
        }

        public void UpdateDataView(PIModel results)
        {
            viewModel.Add(results);
        }

        // VM 做邏輯判斷 view:做渲染 mdoel: DAO資料
        //public MainViewModel viewModel { get; set; }
        //public MainWindow()
        //{
        //    InitializeComponent();
        //    viewModel = new MainViewModel();
        //    DataContext = viewModel;
        //MVVM
        //Model 資料庫
        //View 從後端拿回的資料
        //ViewModel 跟View(畫面)綁定的資料

        private void add_Click(object sender, RoutedEventArgs e)
        {
            this.Debounce(() =>
            {
                int sampleSize = int.Parse(number.Text);
                mainViewPresenter.TakeDataRequest(sampleSize);

            }, 500);

            //if (int.TryParse(number.Text, out int sampleSize))
            //{
            //    if (!results.ContainsKey(sampleSize))
            //    {
            //        var stopWatech = Stopwatch.StartNew();
            //        double pi = PIMission.calculate(sampleSize);
            //        stopWatech.Stop();

            //        var data = new PIModel
            //        {
            //            sample = sampleSize,
            //            time = stopWatech.Elapsed,
            //            value = pi
            //        };

            //        results[sampleSize] = data;
            //        resultList.Add(data);
            //    }
            //    else if (results.ContainsKey(sampleSize))
            //    {
            //        MessageBox.Show("資料已經存在");
            //    }
            //    else
            //    {
            //        MessageBox.Show("請輸入有效數值");
            //    }
            //}
        }
    }
}

// 6/7 跟MVP做結合了解

