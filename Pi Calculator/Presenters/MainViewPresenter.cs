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
using System.Diagnostics;
using System.Threading.Channels;

namespace Pi_Calculator.Presenters
{
    public class MainViewPresenter : IPIMissionPresenter
    {

        private readonly IPIMissionView mainView;
        private CancellationTokenSource cts = new CancellationTokenSource();
        ConcurrentQueue<long> taskQueue = new ConcurrentQueue<long>();
        ConcurrentBag<PIModel> cache = new ConcurrentBag<PIModel>();
        ConcurrentDictionary<long, bool> exsitedSamples = new ConcurrentDictionary<long, bool>();

        public MainViewPresenter(IPIMissionView view) => mainView = view;

        //解決無窮迴圈造成卡死的問題
        Channel<long> channel = Channel.CreateUnbounded<long>();//把資料丟進去後再清空倒出來

        public void StartedMission() 
        {
           //這裡的第一個task只是開一條支線，讓UI不要卡死，但無法達到讓任務平行處理的效果
           // 作業:個別暫停，每一列暫停時，還是可以輸入其他樣本數作處理
           //解決無窮迴圈造成卡死的問題
            Task.Run(async () =>
            {
                Debug.WriteLine("Hello");
                await foreach(long sampleSize in channel.Reader.ReadAllAsync(cts.Token))
                {
                    var sampleCts = new CancellationTokenSource();

                    _ = Task.Run(async () =>
                    {
                        Debug.WriteLine("Hello");

                        var token = cts.Token;
                        //    // 讓calculator 做計算，計算完丟到concurrentBag cache
                        double pi = await PIMission.Calculate(sampleSize, token); // result 是task屬性，這邊最後會取得double結果值
                        if (token.IsCancellationRequested) return;
                        var result = new PIModel
                        {
                            sample = sampleSize,
                            time = DateTime.Now,
                            value = pi,
                            TokenSource = sampleCts
                        };
                        cache.Add(result);
                    });
                   
                }


                // 會有無窮迴圈一直持續跑的問題
                //Task.Run( () =>
                //while (true)
                //{
                //    Debug.WriteLine($"目前柱列中的任務數:{taskQueue.Count}");
                //    if (taskQueue.Count > 0 && taskQueue.TryDequeue(out long sampleSize))
                //    {
                //        Task.Run(async () =>
                //        {
                //            var token = cts.Token;
                //            //    // 讓calculator 做計算，計算完丟到concurrentBag cache
                //            double pi = await PIMission.Calculate(sampleSize, token); // result 是task屬性，這邊最後會取得double結果值
                //            if (token.IsCancellationRequested) return;
                //            var result = new PIModel
                //            {
                //                sample = sampleSize,
                //                time = DateTime.Now,
                //                value = pi
                //            };
                //            cache.Add(result);
                //        });
                //    }
                //}
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
                                                     // this.taskQueue.Enqueue(sampleSize);
            channel.Writer.TryWrite(sampleSize); // 將任務寫入Channel
        }

        public void FetchCompletedMission() 
        {
            List<PIModel> result = cache.ToList();
            cache.Clear();  
            this.mainView.UpdateDataView(result);
        }

        public void StopMission()
        {
            cts.Cancel(); // 所有 token 都會收到取消通知
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
