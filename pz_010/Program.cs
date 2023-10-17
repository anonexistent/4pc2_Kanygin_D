using System.Runtime.InteropServices;

namespace pz_010
{
    class Program
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
            IntPtr regionStartAddress = VirtualAlloc(IntPtr.Zero, MEM_COMMIT, MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
            if (regionStartAddress == IntPtr.Zero)
            {
                Console.WriteLine("fatal eror - cancel for reserv region");
                return;
            }

            Console.WriteLine("region's home " + regionStartAddress);

            // octal(8) number system 00h - 0, 7Fh - 127 (8)
            // 0x7F = 127
            byte value = 127;
            byte[] buffer = new byte[MEM_COMMIT];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = value;
            }

            //Marshal.Copy(buffer, 0, regionStartAddress, buffer.Length);
            // output in octal(8) number system
            Console.WriteLine("fiiling ok (" + value.ToString("X") +")");
            Console.WriteLine("\t\tproofs:");

            DisplayMemoryContents(regionStartAddress, value);

            MemoryBasicInformation memoryInfo;
            int result = VirtualQuery(regionStartAddress, out memoryInfo, Marshal.SizeOf(typeof(MemoryBasicInformation)));
            
            if (result == 0)
            {
                Console.WriteLine("fatal eror - no info");
                VirtualFree(regionStartAddress, 0, MEM_RELEASE);
                return;
            }

            Console.WriteLine(new string('—', 27));
            Console.WriteLine("memory reg size: " + memoryInfo.RegionSize + " байт");
            Console.WriteLine("memory guard: " + memoryInfo.AllocationProtect);
            Console.WriteLine("memory base home: " + memoryInfo.BaseAddress);
            Console.WriteLine("memory status: " + memoryInfo.State);

            VirtualFree(regionStartAddress, 0, MEM_RELEASE);
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