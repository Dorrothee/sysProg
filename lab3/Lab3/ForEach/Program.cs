using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelForEach
{
    class Program
    {
        static double[] data;

        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread is starting.");

            data = new double[100000000];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i;
            }

            //Put negative value in the array
            data[10] = -10;

            //Paralleling with the Parallel.For method using lambda expression
            ParallelLoopResult loopResult = Parallel.ForEach(data, (v, pls) =>
            {
                if (v < 0) pls.Break();
                Console.WriteLine("Value is :" + v);
            });

            //Check if the cycle has ended
            if (!loopResult.IsCompleted)
                Console.WriteLine("ParallelFor was aborted with negative value on iteration " + loopResult.LowestBreakIteration);

            Console.WriteLine("Main() is done.");
            Console.ReadLine();
        }
    }
}
