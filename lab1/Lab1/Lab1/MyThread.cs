using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPriority
{
    internal class MyThread
    {
        public long count;
        public Thread Thrd;
        static bool stop = false;
        static string currentName;

        //Creating the thread without its execution
        public MyThread(string name)
        {
            count = 0;
            Thrd = new Thread(Run);
            Thrd.Name = name;
            currentName = name;
        }

        //Start thread execution
        void Run()
        {
            Console.WriteLine("Thread " + Thrd.Name + " is beginning...");
            do
            {
                count++;
                if (currentName != Thrd.Name)
                {
                    currentName = Thrd.Name;
                    Console.WriteLine("In thread " + currentName + ".");
                }
            } while (stop == false && count < 1e7);
            stop = true;
            Console.WriteLine("\n---Thread " + Thrd.Name + " is completed---\n");
        }
    }
}
