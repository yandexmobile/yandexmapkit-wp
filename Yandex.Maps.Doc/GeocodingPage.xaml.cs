using System.Windows;
using System.Windows.Input;
using Yandex.Maps.Geocoding;
using Yandex.Maps.Geocoding.Interfaces;
using Yandex.Positioning;
using Yandex.WebUtils.Events;

namespace Yandex.Maps.Doc
{
    /// <summary><see cref="GeocodeManager"/> control demonstration.
    /// </summary>
    public partial class GeocodingPage
    {
        private IGeocodeManager _geocodeManager;

        public GeocodingPage()
        {
            InitializeComponent();

            Loaded += GeocodingPageLoaded;
        }

        void GeocodingPageLoaded(object sender, RoutedEventArgs e)
        {
            // init geocode manaager instance and subscribe to its events
            _geocodeManager = Yandex.Maps.Helpers.GeocodeManagerProvider.GetGeocodeManager();

            _geocodeManager.GeocodeCompleted += GeocodeManagerGeocodeCompleted;
            _geocodeManager.GeocodeFailed += GeocodeManagerOnGeocodeFailed;
        }

        private void GeocodeManagerOnGeocodeFailed(object sender, RequestFailedEventArgs<GeocodeRequestParameters> requestFailedEventArgs)
        {
            // Warning! Event is fired in non-UI context
            Dispatcher.BeginInvoke(() => 
                MessageBox.Show(requestFailedEventArgs.Exception.Message));
        }

        void GeocodeManagerGeocodeCompleted(object sender, RequestCompletedEventArgs<GeocodeRequestParameters, Geocoding.Dto.GeocodeResult> e)
        {
            string messageBoxText = e.RequestResults.Addresses[0].Title;

            // Warning! Event is fired in non-UI context
            Dispatcher.BeginInvoke(() => 
                MessageBox.Show(messageBoxText));
        }

        private void MapTap(object sender, GestureEventArgs e)
        {
            Point tapPoint = e.GetPosition(map);
            GeoCoordinate coordinate = map.ViewportPointToCoordinates(new Media.Point(tapPoint.X, tapPoint.Y));
            _geocodeManager.Query(new GeocodeRequestParameters(coordinate, null));
        }
    }
}