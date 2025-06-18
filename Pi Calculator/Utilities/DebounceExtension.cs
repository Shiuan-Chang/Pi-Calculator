using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Pi_Calculator.Utilities
{
    public static class DebounceExtension
    {
        // 擴充方法：使用this在control控鍵上，擴充新的方法。前提是該方須要為靜態

        //Action:委派的一種，允許無參數傳遞(委派允許把方法當參數傳遞)
        static Timer timer = null;
        static Window window = null;
        static Action callback = null;

        public static void Debounce(this Window control, Action action, int delay)
        {

            if (timer != null)
            {
                timer.Change(delay, Timeout.Infinite);
            }
            else
            {
                window = control;
                callback = action;

                timer = new Timer(TimerCallback, null, delay, Timeout.Infinite);
            }
        }

        public static void TimerCallback(object state)
        {
            window.Dispatcher.Invoke(() =>
            {
                callback.Invoke();
            });
        }

    }
}
