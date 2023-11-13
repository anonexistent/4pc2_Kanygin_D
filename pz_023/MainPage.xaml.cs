using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace pz_023
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 550);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //wvMain.Scale = new System.Numerics.Vector3(1000,900, 1);
            wvMain.Source = new Uri(tbSearch.Text);
        }

        async private void wvMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            string ScrollToposition = @"window.scrollTo(0,50);";
            await wvMain.InvokeScriptAsync("eval", new string[] { ScrollToposition });
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            wvMain.Source = new Uri("https://yandex.ru/");

            btnGo.Visibility = Visibility.Collapsed;
            tbSearch.IsEnabled = true;
            btnSearch.IsEnabled = true;
            wvMain.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string a = wvMain.Source.AbsoluteUri;
            var b = new MenuFlyoutItem() { Text = a };
            b.Click += LikesClick;
            ddbMenu.Items.Add(b);
        }

        void LikesClick(object s, RoutedEventArgs e1)
        {
            var ss = ((MenuFlyoutItem)s).Text;
            wvMain.Source = new Uri(ss);
            tbSearch.Text = ss;
        }
    }
}
