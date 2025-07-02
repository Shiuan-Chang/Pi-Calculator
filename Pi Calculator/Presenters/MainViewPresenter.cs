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

namespace Pi_Calculator.Presenters
{
    public class MainViewPresenter : IPIMissionPresenter
    {

        private readonly IPIMissionView mainView;
        ConcurrentQueue<long> taskQueue = new ConcurrentQueue<long>();
        ConcurrentBag<PIModel> cache = new ConcurrentBag<PIModel>();

        public MainViewPresenter(IPIMissionView view) => mainView = view;

        public void StartedMission() 
        {
            Task.Run(() =>
            {

                while (true)
                {
                    if (taskQueue.Count > 0)
                    {
                        taskQueue.TryDequeue(out long sampleSize);
                        
                        // 讓calculator 做計算，計算完丟到concurrentBag cache
                        double pi = PIMission.Calculate(sampleSize).Result; // result 是task屬性，這邊最後會取得double結果值
                        PIModel result = new PIModel
                        {
                            sample = sampleSize,
                            time = DateTime.Now,
                            value = pi
                        };
                        cache.Add(result);
                    }
                }
            });
        }

        public async void SendMissionRequest(long sampleSize)
        {
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
