using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pi_Calculator.Models;

namespace Pi_Calculator.Contract
{
    public class PIMissionContract
    {
        public interface IPIMissionView // View定期從FetchCompletedMission拿資料，放到UpdateDataView，因此定時器是要放在View中
        {
            /// <summary>
            /// 當有完成的任務時會自動呼叫，得到當前所有完成的PiMission Result
            /// </summary>
            /// <param name="results">當前所有計算完後的PiMission結果</param>
            void UpdateDataView(List<PIModel> results);
           
        };
        public interface IPIMissionPresenter 
        {
            /// <summary>
            /// 啟動執行緒任務，不斷接收來自 <c>Queue</c> 的請求，並開始執行計算
            /// </summary>
            void StartedMission();

            /// <summary>
            /// 傳入指定的SampleSize進行PI 計算
            /// </summary>
            /// <param name="sample">決定計算的次數</param>
            void SendMissionRequest(long sample);

            /// <summary>
            /// 發送請求取得完成的等待任務，並自動呼叫 <code>UpdateDataView</code>  得到結果
            /// </summary>
            void FetchCompletedMission();

            /// <summary>
            /// 暫停執行緒任務
            /// </summary>
            //void StopMission();
        
        };
    }
}
