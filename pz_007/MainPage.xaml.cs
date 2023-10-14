using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace pz_007
{


    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
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
                while(true) 
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
}
