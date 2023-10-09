using System.Runtime.InteropServices;
using System.Text;


namespace pz_002
{
    struct OsVersionInfoEx
    {
        public int dwOsVerInfoSize;
        public uint dwMajorVersion;
        public uint dwMinorVersion;
        public uint dwBuildNumber;
        public uint dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szCsdVersion;
        public UInt16 wServicePackMajor;
        public UInt16 wServicePckMinor;
        public UInt16 wSuiteMask;
        public byte wProductType;
        public byte wReserved;
    }

    internal class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetComputerName(StringBuilder nameBuffer, ref int bufferSize);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint GetVersion();

        static void Main(string[] args)
        {
            OsVersionInfoEx osInfo = new();
            osInfo.dwOsVerInfoSize = Marshal.SizeOf(osInfo);
            var majVersion = osInfo.dwMajorVersion;
            var ver2 = Convert.ToUInt32(majVersion & 0x000000ff);
            var minVersion = osInfo.dwMinorVersion;
            var ver3 = Convert.ToUInt32(minVersion & 0x0000ff00) >> 8;
            var build = osInfo.dwBuildNumber;

            if (minVersion < 0x80000000) build = Convert.ToUInt32(minVersion & 0xffff0000) >> 16;

            Console.WriteLine("name: " + System.Net.Dns.GetHostName());
            for (int i = 0; i < 11; i++) Console.Write("—");

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

            Console.WriteLine($"\npath: {path}"); 

            for (int i = 0; i < 11; i++) Console.Write("—");


            Console.WriteLine($"\nver: {GetVersion()}\n" + $"maj: {ver2}\n" + $"min: {ver3}\n" + $"bld: {build}");
        }
    }
}