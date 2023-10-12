using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace pz_006
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (e.Parameter != null) textBlock1.Text = "from " + ((PageInfo)(e.Parameter)).Name;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }        
        
        private void btn_forward_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.Page3));
        }
    }
}
