using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace pz_018
{
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }

        public override string ToString()
        {
            return $"pupl {this.Name} grom caste {this.Group}";
        }
    }

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

 //                   < Button x: Name = "btn2" Width = "100" Height = "40" Content = "btn2" FontFamily = "Verdana"
 //HorizontalAlignment = "Right" VerticalAlignment = "Center" />

            Button a = new Button() { Name = "btn2", Width=100,Height=50,Content="!!! btn2 !!!",
                HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment=VerticalAlignment.Center };
            gridMain.Children.Add(a);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog a = new MessageDialog("bnt clickded");
            a.ShowAsync();
        }
    }
}
