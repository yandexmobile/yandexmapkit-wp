using System;
using System.Windows.Input;

namespace Yandex.Maps.Doc
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Map.EnableLocationService = true;
        }

        private void Button1_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MapPage.xaml", UriKind.Relative));
        }

        private void Button2_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StaticMapPage.xaml", UriKind.Relative));
        }
    }
}