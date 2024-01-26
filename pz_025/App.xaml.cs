using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace pz_025
{
    /// <summary>
    /// Обеспечивает зависящее от конкретного приложения поведение, дополняющее класс Application по умолчанию.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Инициализирует одноэлементный объект приложения. Это первая выполняемая строка разрабатываемого
        /// кода, поэтому она является логическим эквивалентом main() или WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Resuming += App_Resuming;
        }

        private void App_Resuming(object sender, object e)
        {
            ChangeBackgroundColor(Colors.LightGreen);
            ShowMessage("Приложение возобновлено");
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {

                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Загрузить состояние из ранее приостановленного приложения
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                Window.Current.Activate();

                //ContentDialog a = new ContentDialog()
                //{
                //    Title = "launched",
                //    Content = "app has was been is launched now. understud >_< ???",
                //    PrimaryButtonText = "ok"
                //};
                //a.ShowAsync();

                ChangeBackgroundColor(Colors.PaleVioletRed);
                ShowMessage("Приложение запущено");
            }
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                //Frame rootFrame = Window.Current.Content as Frame;
                //var a = (Page)rootFrame.Content;
                //SolidColorBrush buttonBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                //a.Background = buttonBrush;

                //ContentDialog b = new ContentDialog()
                //{
                //    Title = "launched",
                //    Content = "app has was been is ACTIVATED breaking news. understud ._. !!",
                //    PrimaryButtonText = "ok"
                //};

                ChangeBackgroundColor(Colors.LightSkyBlue);
                ShowMessage("Приложение активировано");
            }
            base.OnActivated(args);
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            ChangeBackgroundColor(Colors.LightGray);
            ShowMessage("Приложение приостановлено");

            var deferral = e.SuspendingOperation.GetDeferral();

            //deferral.Complete();

        }

        private async void ShowMessage(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        private void ChangeBackgroundColor(Color color)
        {
            ((Page)((Frame)Window.Current.Content).Content).Background = new SolidColorBrush(color);
        }
    }
}
