using System;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                InfoWriter.WriteToFile(args);

                Console.WriteLine();
                Console.WriteLine("To continue press Enter. To stop press Escape");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
