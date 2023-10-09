 namespace pz_004
{
    internal class Program
    {
        static void WorkerThreadMethod(FakeDataBase str)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"[{i}] worker1 (number one): START [{DateTime.Now}]");
                str.SaveData();
                Console.WriteLine($"[{i}] worker1 (number one): THE END [{DateTime.Now}]");
            }
        }
        static void WorkerThreadMethodClone(FakeDataBase str)
        {
            for(int i = 0;i < 10;i++)
            {
                Console.WriteLine($"[{i}] worker2 (number two): START [{DateTime.Now}]");
                str.SaveData();
                Console.WriteLine($"[{i}] worker1 (numbe one): THE END [{DateTime.Now}]");
            }
        }

        static void Main(string[] args)
        {
            FakeDataBase db = new();
            ThreadStart work1 = new(()=>WorkerThreadMethod(db));
            Thread t1 = new(work1);
            Thread t2 = new(new ThreadStart(()=>WorkerThreadMethodClone(db)));

            t1.Start();
            t2.Start();
        }
    }
}