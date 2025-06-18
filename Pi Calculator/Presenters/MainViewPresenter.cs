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
        public IPIMissionView mainView;
        public MainViewPresenter(IPIMissionView mainWindowView)
        {
            mainView = mainWindowView;
        }

        public void TakeDataRequest(int sampleSize)
        {
            double pi = PIMission.calculate(sampleSize);

            var data = new PIModel
            {
                sample = sampleSize,
                time = DateTime.Now,
                value = pi
            };
            mainView.UpdateDataView(data);
        }

    }
}
