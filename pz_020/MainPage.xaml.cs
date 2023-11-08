using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace pz_020
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        
        private void ButtonNum_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pole.Text = pole.Text + button.Content;
        }

        private void BtnOp_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pole.Text = pole.Text + button.Content;
        }

        private void ravno_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
