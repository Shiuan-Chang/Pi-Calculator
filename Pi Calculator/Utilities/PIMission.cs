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

        public static async Task<double> Calculate(long sampleNumber)
        {
            long insideCircle = 0;                     
            var rand = new ThreadLocal<Random>(()       
                      => new Random(Guid.NewGuid().GetHashCode()));

            await Parallel.ForAsync(0L,sampleNumber,(index, token) =>                      
                {
                    // double 1 = rand.Value!.NextDouble(); 
                    // double 2 = rand.Value!.NextDouble();
                    // double 3 = rand.Value!.NextDouble();
                   // double 4 = rand.Value!.NextDouble();
                   // 

                    double x = rand.Value!.NextDouble(); 
                    double y = rand.Value!.NextDouble();

                    if (x * x + y * y < 1)
                        // 若用lock解決速度會較慢，且搭配題目要求interlocked會更為合適
                        // 但若涉及一個以上的變數，或是涉及到多行程式碼，還是要使用lock
                        Interlocked.Increment(ref insideCircle);

                    return ValueTask.CompletedTask;
                });

            return 4.0 * insideCircle / sampleNumber;
        }


        public static async Task<PIModel> CalculateWithTiming(long sampleSize)
        {
            var start = DateTime.Now;                       

            double pi = await Calculate(sampleSize);  

            return new PIModel
            {
                sample = sampleSize,
                time = start,    
                value = pi
            };
        }

        //public static double calculate(long sampleNumber)
        //{
        //    // 用parallel for處理
        //    int insideCirclePointNum = 0;
        //    Random rand = new Random();

        //    for (int i = 0; i < sampleNumber; i++)
        //    {
        //        double x = rand.NextDouble(); // 介於0-1之間的亂數
        //        double y = rand.NextDouble();

        //        if (x * x + y * y < 1) { insideCirclePointNum++; }
        //    }
        //    return (double)(4.0 * insideCirclePointNum) / sampleNumber; // 前方加double，否則會一直算出整數
        //}
    }
}
