using System.Windows;
using System.Globalization;
using System.Threading;
using System.Windows.Navigation;
using Yandex.Positioning;

namespace Yandex.Maps.Doc
{
    /// <summary><see cref="Yandex.Maps.StaticMap"/> control demonstration.
    /// </summary>
    public partial class StaticMapPage
    {
        public StaticMapPage()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            InitializeComponent();
            pushPin.DataContext = new PushpinDummyViewModel
            {
                Visibility = Visibility.Visible,
                ContentVisibility = Visibility.Collapsed,
                State = PushPinState.Expanded,
                Position = new GeoCoordinate(53.905153, 27.558230)
            };
        }

        /// <summary>Dispose map control explicitly.
        /// </summary>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
                Map.Dispose();
        }
    }
}