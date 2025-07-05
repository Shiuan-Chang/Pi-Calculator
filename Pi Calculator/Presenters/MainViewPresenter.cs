using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pi_Calculator.Utilities;
using Pi_Calculator.Models;
using static Pi_Calculator.Contract.PIMissionContract;
using System.Collections;
using System.Collections.Concurrent;
using System.Windows;

namespace Pi_Calculator.Presenters
{
    public class MainViewPresenter : IPIMissionPresenter
    {

        private readonly IPIMissionView mainView;
        ConcurrentQueue<long> taskQueue = new ConcurrentQueue<long>();
        ConcurrentBag<PIModel> cache = new ConcurrentBag<PIModel>();
        ConcurrentDictionary<long, bool> exsitedSamples = new ConcurrentDictionary<long, bool>();

        public MainViewPresenter(IPIMissionView view) => mainView = view;

        public void StartedMission() 
        {
            Task.Run(() =>
            {

                while (true)
                {
                    if (taskQueue.Count > 0)
                    {
                        if (taskQueue.TryDequeue(out long sampleSize)) 
                        {
                            Task.Run(async () =>
                            {
                                // 讓calculator 做計算，計算完丟到concurrentBag cache
                                double pi = await PIMission.Calculate(sampleSize); // result 是task屬性，這邊最後會取得double結果值
                                var result = new PIModel
                                {
                                    sample = sampleSize,
                                    time = DateTime.Now,
                                    value = pi
                                };
                                cache.Add(result);
                            });
                        }
                    }
                }
            });
        }

        public async void SendMissionRequest(long sampleSize)
        {
            if (exsitedSamples.ContainsKey(sampleSize))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"樣本數 {sampleSize} 已處理過，請勿重複提交！");
                });
                return;
            }

            exsitedSamples.TryAdd(sampleSize, true); // 標記為已提交
            this.taskQueue.Enqueue(sampleSize);
        }

        public void FetchCompletedMission() 
        {
            List<PIModel> result = cache.ToList();
            cache.Clear();  
            this.mainView.UpdateDataView(result);
        }


        //public IPIMissionView mainView;
        //public MainViewPresenter(IPIMissionView mainWindowView)
        //{
        //    mainView = mainWindowView;
        //}

        //public void SendPIMissionRequest(long sampleSize)
        //{
        //    double pi = PIMission.calculate(sampleSize);

        //    var data = new PIModel
        //    {
        //        sample = sampleSize,
        //        time = DateTime.Now,
        //        value = pi
        //    };
        //    mainView.UpdateDataView(data);
        //}
    } 
        
}
