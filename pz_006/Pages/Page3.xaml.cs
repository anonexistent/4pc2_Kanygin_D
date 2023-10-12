using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace pz_006.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Page3 : Page
    {
        public Page3()
        {
            this.InitializeComponent();
        }

        private void btn_forward_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page4));
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page2));
        }
    }
}
