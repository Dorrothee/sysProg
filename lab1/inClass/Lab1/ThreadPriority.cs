using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPriority
{
    internal class MyThread
    {
        public int Count;
        public Thread Thrd;
        static bool stop = false;
        static string currentName;
        //Creating of the thread without its execution
        public MyThread(string name)
        {
            Count = 0;
            Thrd = new Thread(Run);
            Thrd.Name = name;
            currentName = name;
        }

        //Start thread execution
        void Run()
        {
            Console.WriteLine("Thread " + Thrd.Name + " is beginning.");
            do
            {
                Count++;
                if (currentName != Thrd.Name)
                {
                    currentName = Thrd.Name;
                    Console.WriteLine("In thread " + currentName);
                }
            } while (stop == false && Count < 1e9);
            stop = true;
            Console.WriteLine("Thread " + Thrd.Name + " is completed.");
        }
    }
}
