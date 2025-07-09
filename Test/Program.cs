using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            for(int i=0;i<10;i++)
            {
                Console.WriteLine("Hello");
                await Task.Delay(100000000);
            }


            Console.ReadKey();

        }
    }
}
