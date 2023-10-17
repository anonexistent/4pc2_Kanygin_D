using System.Runtime.InteropServices;

namespace pz_009
{
    class Program
    {
        #region SystemMethods
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualFree(IntPtr lpAddress, uint dwSize, uint dwFreeType);
        #endregion

        #region SystemConst
        // page size (4k)
        const uint MEM_COMMIT = 0x1000;
        // operation code
        const uint MEM_RELEASE = 0x8000;
        // rw code
        const uint PAGE_READWRITE = 0x04;
        #endregion

        // intPtr - data type for pointer (mb unknown data type)
        static IntPtr region1StartAddress = IntPtr.Zero; // Адрес начала первого региона памяти
        static IntPtr region2StartAddress = IntPtr.Zero; // Адрес начала второго региона памяти

        static void Main(string[] args)
        {
            // 2 pieces
            int regionSize = 2 * (int)Math.Pow(2, 12);
            // sys meth for res regs
            ReserveMemoryRegions(regionSize);

            if (region1StartAddress == IntPtr.Zero || region2StartAddress == IntPtr.Zero)
            {
                Console.WriteLine("fatal eror: cancel by try regs");
                return;
            }

            Console.WriteLine("area1 home: " + region1StartAddress);
            Console.WriteLine("area2 home: " + region2StartAddress);

            ZeroMemory(region1StartAddress, regionSize);
            Console.WriteLine("reg1 set 0");

            int number = -1;
            while(number < 0)
            {
                Console.Write(">> waiting in number (0-127): ");
                int temp = Convert.ToInt32(Console.ReadLine());
                if (temp >= 0 & temp <= 127) number = temp;
            }

            // sys meth for write user data
            // start, how much from all block in reg wee need fill, user data
            FillMemory(region2StartAddress, regionSize, number);
            Console.WriteLine("reg2 done for " + number);

            Console.WriteLine("reg1 has:");
            // fill but is reversed
            DisplayMemoryContents(region1StartAddress, regionSize);

            Console.WriteLine("reg2 has:");
            DisplayMemoryContents(region2StartAddress, regionSize);

            // congratulations!!! we are free
            VirtualFree(region1StartAddress, 0, MEM_RELEASE);
            VirtualFree(region2StartAddress, 0, MEM_RELEASE);
        }

        static void ReserveMemoryRegions(int regionSize)
        {
            region1StartAddress = VirtualAlloc(IntPtr.Zero, (uint)regionSize, MEM_COMMIT, PAGE_READWRITE);
            if (region1StartAddress == IntPtr.Zero)
            {
                Console.WriteLine("fatal eror: cancel by try res reg1");
                return;
            }

            region2StartAddress = VirtualAlloc(IntPtr.Zero, (uint)regionSize, MEM_COMMIT, PAGE_READWRITE);
            if (region2StartAddress == IntPtr.Zero)
            {
                Console.WriteLine("fatal eror: cancel by try res reg2");
                VirtualFree(region1StartAddress, 0, MEM_RELEASE);
                return;
            }
        }

        static void ZeroMemory(IntPtr startAddress, int size)
        {
            byte[] buffer = new byte[size];
            Marshal.Copy(buffer, 0, startAddress, size);
        }

        static void FillMemory(IntPtr startAddress, int size, int number)
        {
            byte[] buffer = new byte[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = (byte)number;
            }
            Marshal.Copy(buffer, 0, startAddress, size);
        }

        static void DisplayMemoryContents(IntPtr startAddress, int size)
        {
            byte[] buffer = new byte[size];
            Marshal.Copy(startAddress, buffer, 0, size);
            for (int i = 0; i < size; i++)
            {
                Console.Write(buffer[i] + " ");
            }
            Console.WriteLine();
        }
    }
}