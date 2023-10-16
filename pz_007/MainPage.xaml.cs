using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace pz_007
{
    public sealed partial class MainPage : Page
    {
        public struct MemoryStatusEx
        {
            public uint uiLength;
            public uint uiMemoryLoad;
            public ulong ulTotalPhys;
            public ulong ulAvailPhys;
            public ulong ulTotalPageFile;
            public ulong ulAvailPageFile;
            public ulong ulTotalVirtual;
            public ulong ulAvailVirtual;
            public ulong ulAvailExtendedVirtual;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll")]
        public static extern bool GlobalMemoryStatusEx(ref MemoryStatusEx lpBuffer);

        public MainPage()
        {
            this.InitializeComponent();

            Start();
        }

        private async void Start()
        {
            await Task.Delay(3000);

            MemoryStatusEx ms = new MemoryStatusEx();
            ms.uiLength = (uint)Marshal.SizeOf(ms);

            if(GlobalMemoryStatusEx(ref ms))
            {
                await Task.Delay(10);
                tbPh.Text = ms.ulTotalPhys + " byte";
                tbPhA.Text = ms.ulAvailPhys + " byte";
                tbPod.Text = ms.ulTotalPageFile + " byte";
                tbPodA.Text = ms.ulAvailPageFile + " byte"; ;
                tbVirt.Text = ms.ulTotalVirtual + " byte";
                tbVirtA.Text = ms.ulAvailVirtual + " byte";
                tbProc.Text = ms.ulAvailExtendedVirtual + " byte";
                tbLoad.Text = ms.uiMemoryLoad + " %";

            }

        }
    }
}
