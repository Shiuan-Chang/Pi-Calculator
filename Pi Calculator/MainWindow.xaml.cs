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
using System.Windows.Threading;
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
        private Timer timer;
        private bool isRunning = false;


        public MainWindow()
        {
            InitializeComponent();
            mainViewPresenter = new MainViewPresenter(this);
            DataContext = viewModel;
            mainViewPresenter.StartedMission();

            timer = new Timer((state) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    mainViewPresenter.FetchCompletedMission();
                });
            }, null, 0, 1000);
        }

        public void UpdateDataView(List<PIModel> results)
        {
            foreach (var result in results)
            {
                viewModel.Add(result);
            }
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


        //HW:將 start/pause 兩者作結合， 如果是 Start Mission 就開始計算， 如果是 Pause Mission 就暫停計算
        private async void add_Click(object sender, RoutedEventArgs e)
        {
            this.Debounce(async () => 
            {
                if (long.TryParse(number.Text, out long sampleSize) && sampleSize > 0)
                     mainViewPresenter.SendMissionRequest(sampleSize);
                else MessageBox.Show("請輸入正整數");
            }, 500);
        }

        private void pause_btn_Click(object sender, RoutedEventArgs e)
        {
            mainViewPresenter.StopMission();
        }

        private void startPause_Click(object sender, RoutedEventArgs e)
        {
            if (!isRunning)
            {
                mainViewPresenter.RestartMission(); // 新啟動 token & 任務讀取迴圈
                startPause.Content = "Pause Mission";
                isRunning = true;
            }
            else
            {
                mainViewPresenter.StopMission(); // 停止 Channel reader
                startPause.Content = "Start Mission";
                isRunning = false;
            }

        }



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
// 6/7 跟MVP做結合了解

