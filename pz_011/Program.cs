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
            IntPtr address = VirtualAlloc(IntPtr.Zero, 4096, MEM_COMMIT, PAGE_READWRITE);

            if (address == IntPtr.Zero)
            {
                Console.WriteLine("fatal eror: region is not");
                return;
            }

            MemoryBasicInformation memInfo;
            int result = VirtualQuery(address, out memInfo, Marshal.SizeOf(typeof(MemoryBasicInformation)));
            if (result == 0)Console.WriteLine("fatal eror: no info");
            Console.WriteLine("(AllocationProtect) " + memInfo.AllocationProtect + "\t(Protect) " + memInfo.Protect);

            // 2 type output
            Console.WriteLine("region home: " + address.ToString() 
                //+ $" (0x{address.ToString("X")})"
                );

            // current protect
            uint oldProtect;

            // try change guard
            uint newProtect = 0x04;
            if (!VirtualProtect(address, 4096, newProtect, out oldProtect))
            {
                DisplayErrorMessage("change guard fail");
                VirtualFree(address, 0, 0);
                return;
            }
            Console.WriteLine("new guard");

            result = VirtualQuery(address, out memInfo, Marshal.SizeOf(typeof(MemoryBasicInformation)));
            if (result == 0) Console.WriteLine("fatal eror: no info");
            Console.WriteLine("(AllocationProtect) " + memInfo.AllocationProtect + "\t(Protect) " + memInfo.Protect);

            // size - 4k, but 128 enough for demo
            DisplayMemoryContents(address, 128);

            // try write (need exeprion if it s work)
            try
            {
                // write from start bytes
                Marshal.WriteInt64(address, 4584323413317295619);
                Console.WriteLine(">>write status - ok");
            }
            catch (AccessViolationException ex)
            {
                DisplayErrorMessage(ex.Message);
            }

            // size - 4k, but 128 enough for demo
            DisplayMemoryContents(address, 128);

            VirtualFree(address, 0, 0);

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
        static void DisplayErrorMessage(string message)
        {
            uint errorCode = GetLastError(); string errorMessage;

            uint result = FormatMessage(0x00001000 | 0x00000200 | 0x00000100, 
                IntPtr.Zero, errorCode, 0, out errorMessage, 0, IntPtr.Zero);

            if (result != 0) Console.WriteLine("Ошибка: " + message + " - " + errorMessage.Trim());
            else Console.WriteLine("Ошибка: " + message + " - код ошибки: " + errorCode);
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
            for (int i = 0; i < size; i++) buffer[i] = (byte)number;
            Marshal.Copy(buffer, 0, startAddress, size);
        }

        static void DisplayMemoryContents(IntPtr startAddress, int size)
        {
            byte[] buffer = new byte[size];
            Marshal.Copy(startAddress, buffer, 0, size);
            for (int i = 0; i < size; i++) Console.Write(buffer[i] + " ");
            Console.WriteLine();
        }
    }
}