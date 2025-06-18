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
        public interface IPIMissionView
        {
            void UpdateDataView(PIModel results);
        };
        public interface IPIMissionPresenter 
        {
            void TakeDataRequest(int sample);
        
        };
    }
}
