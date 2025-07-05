using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Pi_Calculator.Models;

namespace Pi_Calculator
{
    public class MainViewModel // VM主要管理資料流，command可以拿掉
    {
        public String Title { get; set; } = "PI Calcurator";
        public ObservableCollection<PIModel> resultList { get; set; } = new ObservableCollection<PIModel>(); // MVVM架構，不用再清空，ObservableCollection

        public void Add(PIModel item)
        {
                resultList.Add(item);   
        }
    }
}
