using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pi_Calculator.Models;

namespace Pi_Calculator.Utilities
{
    
    public static class PIMission
    {
        // presenter 呼叫PIMission
        //calculateWithTiming 不會存在
        public static PIModel calculateWithTiming(int sampleSize)
        {
            double pi = calculate(sampleSize);
            return new PIModel
            {
                sample = sampleSize,
                time = DateTime.Now,
                value = pi
            };
        }

        public static double calculate(int sampleNumber)
        {
            // 用parallel for處理
            int insideCirclePointNum = 0;
            Random rand = new Random();

            for (int i = 0; i < sampleNumber; i++)
            {
                double x = rand.NextDouble(); // 介於0-1之間的亂數
                double y = rand.NextDouble();

                if (x * x + y * y < 1) { insideCirclePointNum++; }
            }
            return (double)(4 * insideCirclePointNum) / sampleNumber; // 前方加double，否則會一直算出整數
        }
    }
}
