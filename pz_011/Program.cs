using System.Runtime.InteropServices;

namespace pz_011
{
    internal class Program
    {
        #region SystemMethods
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualFree(IntPtr lpAddress,
            uint dwSize, uint dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQuery(IntPtr lpAddress,
            out MemoryBasicInformation lpBuffer, int dwLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualProtect(IntPtr pvAdress,
            uint dwSize, uint flNewProtect, out uint pflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint GetLastError();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint FormatMessage(uint dwFlags, 
            IntPtr lpSource, uint dwMessageId, uint dwLanguageId, out string lpBuffer, uint nSize, IntPtr Arguments);

        #endregion

        #region SystemConst
        // give page
        const uint MEM_RESERVE = 0x2000;
        // page size (4k)
        const uint MEM_COMMIT = 0x1000;
        // operation code
        const uint MEM_RELEASE = 0x8000;
        // rw code
        const uint PAGE_READWRITE = 0x04;
        #endregion

        // storage
        public struct MemoryBasicInformation
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        static void Main(string[] args)
        {
            // give me ur memory
            IntPtr ptr = Marshal.AllocHGlobal(1024);

            DisplayMemoryContents(ptr, 1024);

            /*
            PAGE_EXECUTE                0x10 (16)
            PAGE_EXECUTE_READ           0x20
            PAGE_EXECUTE_READWRITE      0x40
            PAGE_EXECUTE_WRITECOPY      0x80 (128)
            PAGE_NOACCESS               0x01
            PAGE_READONLY               0x02
            PAGE_READWRITE              0x04
            PAGE_WRITECOPY              0x08
            PAGE_TARGETS_INVALID        0x40000000
            PAGE_TARGETS_NO_UPDATE      0x40000000
             */
            uint oldProtect;
            if (!VirtualProtect(ptr, 1024, 0x40, out oldProtect))
            {
                Console.WriteLine(oldProtect.ToString());
                // обработка ошибки
                uint error = GetLastError();
                string message;
                FormatMessage(0x1300, IntPtr.Zero, error, 0, out message, 0, IntPtr.Zero);
                Console.WriteLine("Ошибка: {0}", message);
                return;
            }

            // использование памяти

            // изменение защиты памяти на исходную
            if (!VirtualProtect(ptr, 1024, oldProtect, out oldProtect))
            {
                // обработка ошибки
                uint error = GetLastError();
                string message;
                FormatMessage(0x1300, IntPtr.Zero, error, 0, out message, 0, IntPtr.Zero);
                Console.WriteLine("Ошибка: {0}", message);
                return;
            }

            // освобождение памяти
            Marshal.FreeHGlobal(ptr);

            #region Old
            //IntPtr region = VirtualAlloc(IntPtr.Zero, MEM_COMMIT, MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
            //if (region == IntPtr.Zero)
            //{
            //    Console.WriteLine("fatal eror: region is not");
            //}

            //Console.WriteLine(region);

            //DisplayMemoryContents(region, 127);

            //MemoryBasicInformation memInfo;
            //int result = VirtualQuery(region, out memInfo, Marshal.SizeOf(typeof(MemoryBasicInformation)));

            //if (result == 0)
            //{
            //    Console.WriteLine("fatal eror - no info");
            //}

            //Console.WriteLine("protect: " + memInfo.Protect);
            //Console.WriteLine("all. protect: " + memInfo.AllocationProtect);

            //VirtualProtect(region, 127, 3, (IntPtr)memInfo.AllocationProtect);

            //Console.WriteLine("protect: " + memInfo.Protect);
            //Console.WriteLine("all. protect: " + memInfo.AllocationProtect);
            #endregion
        }

        static void ReserveMemoryRegions(int regionSize)
        {
            //region1StartAddress = VirtualAlloc(IntPtr.Zero, (uint)regionSize, MEM_COMMIT, PAGE_READWRITE);
            //if (region1StartAddress == IntPtr.Zero)
            //{
            //    Console.WriteLine("fatal eror: cancel by try res reg1");
            //    return;
            //}

            //region2StartAddress = VirtualAlloc(IntPtr.Zero, (uint)regionSize, MEM_COMMIT, PAGE_READWRITE);
            //if (region2StartAddress == IntPtr.Zero)
            //{
            //    Console.WriteLine("fatal eror: cancel by try res reg2");
            //    VirtualFree(region1StartAddress, 0, MEM_RELEASE);
            //    return;
            //}
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