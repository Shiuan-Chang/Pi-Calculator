using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi_Calculator.Models
{
    public class PIModel
    {
        public long sample { get; set; }
        public DateTime time { get; set; }
        public double value { get; set; }
        public CancellationTokenSource? TokenSource { get; set; }
    }
}
