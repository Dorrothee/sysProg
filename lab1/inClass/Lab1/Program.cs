using ThreadPriority;
class Program
{
    static void Main(string[] args)
    {
        MyThread mt1 = new MyThread("With High priority.");
        MyThread mt2 = new MyThread("with Low priority.");
        //Priorities setting for threads
        //AboveNormal
        mt1.Thrd.Priority = System.Threading.ThreadPriority.AboveNormal;
        //BelowNormal
        mt2.Thrd.Priority = System.Threading.ThreadPriority.BelowNormal;
        //Threads execution
        mt1.Thrd.Start();
        mt2.Thrd.Start();
        mt1.Thrd.Join();
        mt2.Thrd.Join();
        Console.WriteLine();
        Console.WriteLine("Thread " + mt1.Thrd.Name + " counted to " + mt1.Count);
        Console.WriteLine("Thread " + mt2.Thrd.Name + " counted to " + mt2.Count);
        Console.ReadLine();
    }
}