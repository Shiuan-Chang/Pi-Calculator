using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi_Calculator
{
    public static class Calculator
    {
        public static double calculate(int sampleNumber) 
        {
            int insideCirclePointNum = 0;
            Random rand = new Random();

            for (int i = 0; i < sampleNumber; i++) 
            {
                double x = rand.NextDouble(); // 介於0-1之間的亂數
                double y = rand.NextDouble();

                if (x*x + y*y < 1) { insideCirclePointNum++; }
            }
            return (double)( 4* insideCirclePointNum) / sampleNumber; // 前方加double，否則會一直算出整數
        }
    }
}
