using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class MyThread
    {
        public int Count;
        public Thread newThrd;
        public MyThread(string name, int num)
        {
            Count = 0;
            //Creating the thread from the method of the object
            newThrd = new Thread(this.Run);
            newThrd.Name = name;
            //Start thread execution with the parameter num
            newThrd.Start(num);
        }

        //Entry point into the stream
        public void Run(object num)
        {
            Console.WriteLine(newThrd.Name + " exequting to number " + num);
            do
            {
                Thread.Sleep(500);
                Console.WriteLine("In the thread " + newThrd.Name + ", Count=" + Count);
                Count++;
            } while (Count < (int)num);
            Console.WriteLine(newThrd.Name + " is completed.");
        }
    }
}
