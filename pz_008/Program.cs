using System.Diagnostics;
using System.Runtime.InteropServices;

namespace pz_008
{
    class Program
    {            
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr HeapCreate(UInt32 flOptions, UIntPtr dwInitialSize, UIntPtr dwMaximumSize);
        public static void Main(string[] args)
        {
            ulong i = 0;

            Thread t1 = new Thread(() =>
            {
                while (true)
                {
                    i++;
                    Console.WriteLine("thread 1 work");
                    Thread.Sleep(150);
                    HeapCreate(0, (UIntPtr)64, (UIntPtr)128);
                }
            });
            Thread t2 = new Thread(() =>
            {
                while (true)
                {
                    i++;
                    Console.WriteLine("thread 2 work");
                    Thread.Sleep(200);
                    HeapCreate(0, (UIntPtr)64, (UIntPtr)128);
                }
            });
            t1.Start();
            t2.Start();
        }
    }
}