using System.Diagnostics.Metrics;
using System.Threading;
using ThreadPriority;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter what task you want to run:");
        Console.WriteLine("1 - Ordinary task (static program)");
        Console.WriteLine("2 - High-level task (UI)");
        Console.WriteLine("3 - Exit");
        Console.Write("Enter your choice: ");
        int input = GetValidatedInput(1, 3);

        switch (input)
        {
            case 1:
                //Creating 3 instances of the MyThread
                MyThread mt1 = new MyThread("With the Normal priority");
                MyThread mt2 = new MyThread("With The Above Normal priority");
                MyThread mt3 = new MyThread("With the Highest priority");

                //Setting priorities for threads according to the description
                mt1.Thrd.Priority = System.Threading.ThreadPriority.Normal;
                mt2.Thrd.Priority = System.Threading.ThreadPriority.AboveNormal;
                mt3.Thrd.Priority = System.Threading.ThreadPriority.Highest;

                //Threads execution
                mt1.Thrd.Start();
                mt2.Thrd.Start();
                mt3.Thrd.Start();
                mt1.Thrd.Join();
                mt2.Thrd.Join();
                mt3.Thrd.Join();

                //CPU consumption calculation
                long cpuDistribution = mt1.count + mt2.count + mt3.count;
                double t1Percentage = (double)mt1.count / cpuDistribution * 100;
                double t2Percentage = (double)mt2.count / cpuDistribution * 100;
                double t3Percentage = (double)mt3.count / cpuDistribution * 100;

                //The result output
                Console.WriteLine();
                Console.WriteLine($"| {mt1.Thrd.Name} | Count to {mt1.count} | With CPU concumption of {t1Percentage:F2}% |");
                Console.WriteLine($"| {mt2.Thrd.Name} | Count to {mt2.count} | With CPU concumption of {t2Percentage:F2}% |");
                Console.WriteLine($"| {mt3.Thrd.Name} | Count to {mt3.count} | With CPU concumption of {t3Percentage:F2}% |");
                Console.ReadLine();
                break;
            case 2:
                Console.WriteLine("\nEnter the number of threads to execute:");
                int tNum = Convert.ToInt32(Console.ReadLine());

                //List to handle threads
                List<MyThread> threadList = new List<MyThread>();

                for (int i = 1; i <= tNum; i++)
                {
                    Console.WriteLine("\n---Priorities for the thread execution---");
                    Console.WriteLine("1 - Highest");
                    Console.WriteLine("2 - AboveNormal");
                    Console.WriteLine("3 - Normal");
                    Console.WriteLine("4 - BelowNormal");
                    Console.WriteLine("5 - Lowest");
                    Console.WriteLine($"\nEnter the priority for thread {i}:");

                    int priorityChoice = GetValidatedInput(1, 5);

                    //Create thread
                    MyThread mt = new MyThread($"Thread-{i}");

                    //Set priority
                    switch (priorityChoice)
                    {
                        case 1:
                            mt.Thrd.Priority = System.Threading.ThreadPriority.Highest;
                            break;
                        case 2:
                            mt.Thrd.Priority = System.Threading.ThreadPriority.AboveNormal;
                            break;
                        case 3:
                            mt.Thrd.Priority = System.Threading.ThreadPriority.Normal;
                            break;
                        case 4:
                            mt.Thrd.Priority = System.Threading.ThreadPriority.BelowNormal;
                            break;
                        case 5:
                            mt.Thrd.Priority = System.Threading.ThreadPriority.Lowest;
                            break;
                    }

                    threadList.Add(mt);
                }

                Console.WriteLine();
                foreach (var thread in threadList)
                {
                    thread.Thrd.Start();
                }
                foreach (var thread in threadList)
                {
                    thread.Thrd.Join();
                }

                //Display results
                Console.WriteLine("\n=============================");
                Console.WriteLine("| Thread Name | Count        |");
                Console.WriteLine("=============================");

                foreach (var thread in threadList)
                {
                    Console.WriteLine($"| {thread.Thrd.Name} | Count to {thread.count}");
                }

                Console.WriteLine("=============================");
                Console.WriteLine("\nAll threads have finished execution.");
                Console.ReadLine();

                break;
        }
    }

    static int GetValidatedInput(int min, int max)
    {
        int value;

        while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max)
        {
            Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
        }

        return value;
    }
}