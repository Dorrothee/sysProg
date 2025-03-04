using System;
using System.Diagnostics;
using System.Linq;

namespace SequentialProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose data type: \n1. Integer \n2. Double");
                int typeChoice = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the number of elements in the array:");
                int size = int.Parse(Console.ReadLine());

                Console.WriteLine("Choose the function to apply:");
                Console.WriteLine("1. x = x / 10");
                Console.WriteLine("2. x = x / pi");
                Console.WriteLine("3. x = e^x / x^pi");
                Console.WriteLine("4. x = e^(pi*x) / x^pi");
                int functionChoice = int.Parse(Console.ReadLine());

                int[] arrayInt;
                double[] arrayDouble;
                Stopwatch sw = Stopwatch.StartNew();

                if (typeChoice == 1)
                {
                    arrayInt = Enumerable.Range(1, size).ToArray();
                    for (int i = 0; i < size; i++)
                    {
                        arrayInt[i] = Compute(arrayInt[i], functionChoice);
                    }
                }
                else if (typeChoice == 2)
                {
                    arrayDouble = new double[size];
                    Random rand = new Random();
                    for (int i = 0; i < size; i++)
                    {
                        arrayDouble[i] = rand.NextDouble() * 1000;
                        arrayDouble[i] = Compute(arrayDouble[i], functionChoice);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }
                sw.Stop();
                Console.WriteLine("General time processing= " + sw.Elapsed.TotalSeconds + " seconds.");

                Console.WriteLine("Do you want to run again? (y/n)");
                string choice = Console.ReadLine().ToLower();
                if (choice != "y")
                {
                    break;
                }
            }
        }

        static int Compute(int x, int functionChoice)
        {
            switch (functionChoice)
            {
                case 1: return x / 10;
                case 2: return (int)(x / Math.PI);
                case 3: return (int)(Math.Exp(x) / Math.Pow(x, Math.PI));
                case 4: return (int)(Math.Exp(Math.PI * x) / Math.Pow(x, Math.PI));
                default: return x;
            }
        }

        static double Compute(double x, int functionChoice)
        {
            switch (functionChoice)
            {
                case 1: return x / 10;
                case 2: return x / Math.PI;
                case 3: return Math.Exp(x) / Math.Pow(x, Math.PI);
                case 4: return Math.Exp(Math.PI * x) / Math.Pow(x, Math.PI);
                default: return x;
            }
        }
    }
}
