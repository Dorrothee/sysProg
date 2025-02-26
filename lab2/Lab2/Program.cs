using System.Threading;

class Program {

    //--- MAIN ---
    private static void Main(string[] args)
    {
        Console.WriteLine("--- FIRST PART ---");
        Console.WriteLine("Main Thread is starting");
        //Create and run two tasks in parallel
        Task tsk1 = Task.Run(() => MyTask());
        Task tsk2 = Task.Run(() => MyTask());
        //Wait for both tasks to complete
        Task.WaitAll(tsk1, tsk2);
        Console.WriteLine("Main() is done.\n\n");


        Thread.Sleep(500);


        Console.WriteLine("--- SECOND PART ---");
        Console.Write("Enter number to get sum of: ");
        int sum = int.Parse(Console.ReadLine());
        Console.Write("\nEnter message to process: ");
        string message = Console.ReadLine();

        Console.WriteLine("Main Thread is starting");

        //Parallel tasks using lambda expressions for arguments
        Parallel.Invoke(
            () =>
            {
                Task<int> task1 = new Task<int>(() => ComputeSum(sum));
                task1.Start();
                Console.WriteLine("Sum Computation Result: " + task1.Result);
            },
            () =>
            {

                Task<string> task2 = new Task<string>((obj) => ProcessMessage((string)obj), message);
                task2.Start();
                Console.WriteLine("Message Processing Result: " + task2.Result);
            }
        );
        Console.WriteLine("Main() is done.");
        Console.ReadLine();
    }


    //---FIRST---
    static void MyTask()
    {
        int taskId = Task.CurrentId ?? -1;
        int delay = taskId * 200;
        Console.WriteLine("MyTask №" + taskId + " has started.");
        for (int count = 0; count < 10; count++)
        {
            Thread.Sleep(delay);
            Console.WriteLine("In the method MyTask() №" + taskId + " counter = " + count);
        }
        Console.WriteLine("MyTask() №" + taskId + " is done.");
    }


    //--- SECOND ---
    //First computational task
    static int ComputeSum(int n)
    {
        int sum = 0;
        for (int i = 1; i <= n; i++)
        {
            Thread.Sleep(200);
            sum += i;
        }
        return sum;
    }
    //Second computational task
    static string ProcessMessage(string message)
    {
        Thread.Sleep(200);
        return "Processed Message: " + message.ToUpper();
    }
}
