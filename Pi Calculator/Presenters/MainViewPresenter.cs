using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pi_Calculator.Utilities;
using Pi_Calculator.Models;
using static Pi_Calculator.Contract.PIMissionContract;

namespace Pi_Calculator.Presenters
{
    public class MainViewPresenter : IPIMissionPresenter
    {

        private readonly IPIMissionView mainView;

        public MainViewPresenter(IPIMissionView view) => mainView = view;

        public async Task TakeDataRequest(long sampleSize)
        {
            PIModel data = await PIMission.CalculateWithTiming(sampleSize);
            mainView.UpdateDataView(data);   
        }

        //public IPIMissionView mainView;
        //public MainViewPresenter(IPIMissionView mainWindowView)
        //{
        //    mainView = mainWindowView;
        //}

        //public void TakeDataRequest(long sampleSize)
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
