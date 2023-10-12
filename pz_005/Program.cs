using System;

namespace pz_005
{
    class Program
    {
        static int value = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("\t\tточка невозврата:\n");

            var generatorThread = new Thread(GenerateProgression);
            var printerThread = new Thread(PrintProgression);

            generatorThread.Start();
            printerThread.Start();

            generatorThread.Join();
            printerThread.Join();
        }

        static void GenerateProgression()
        {
            int n = 2;

            while (true)
            {
                if (value == 64)
                {
                    Console.WriteLine("прогрессия отдыхает");
                    Thread.Sleep(new Random().Next(100, 1_000));
                    Console.WriteLine("прогрессия отдохнула");
                }

                if (value == 1024)
                {
                    Console.WriteLine(">>крит значение");
                    return;
                }

                value *= n;
                Thread.Sleep(500);
            }
        }

        static void PrintProgression()
        {
            while (true)
            {
                Console.WriteLine($"[{DateTime.Now}]\t{value}");
                Thread.Sleep(250);
            }
        }
    }
}