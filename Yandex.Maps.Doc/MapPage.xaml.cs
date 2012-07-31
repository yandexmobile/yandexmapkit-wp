using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Yandex.Maps.Events;
using Yandex.Positioning;
using System.Linq;

namespace Yandex.Maps.Doc
{
    /// <summary><see cref="Yandex.Maps.Map"/> control demonstration.
    /// </summary>
    public partial class MapPage
    {
        public MapPage()
        {
            // set current culture to force map to show localized data
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            
            InitializeComponent();
            
            var viewModels = new[]
                                 {
                                     new PushpinDummyViewModel
                                         {
                                             ContentVisibility = Visibility.Visible,
                                             Position = new GeoCoordinate(53.905153, 27.558230),
                                         },
                                     new PushpinDummyViewModel
                                         {
                                             ContentVisibility = Visibility.Collapsed,
                                             State = PushPinState.Expanded,
                                             Position = new GeoCoordinate(53.916153, 27.569230),
                                         },
                                     new PushpinDummyViewModel
                                         {
                                             ContentVisibility = Visibility.Collapsed,
                                             State = PushPinState.Collapsed,
                                             Position = new GeoCoordinate(53.913153, 27.567230),
                                         }
                                 };
            mapItemsControl.ItemsSource = viewModels;

            var geoCoordinatesRect = new GeoCoordinatesRect(viewModels.Select(m => m.Position));
            PushpinArea.SetValue(MapLayer.LocationRectangleProperty, geoCoordinatesRect);
        }

        /// <summary>Zoom in to closest integer zoom level.
        /// </summary>
        private void Button1Tap(object sender, EventArgs eventArgs)
        {
            map.ZoomIn();
        }

        /// <summary>Zoom out to closest integer zoom level.
        /// </summary>
        private void Button2Tap(object sender, EventArgs eventArgs)
        {
            map.ZoomOut();
        }

        /// <summary>Jump to user's location.
        /// </summary>
        /// <remarks>Map's UseLocation property should be true.</remarks>
        private void Button3Tap(object sender, EventArgs eventArgs)
        {
            GeoPositionStatus status = map.JumpToCurrentLocation();
            switch (status)
            {
                case GeoPositionStatus.Disabled:
                    MessageBox.Show("Disabled");
                    break;
                case GeoPositionStatus.NoData:
                    MessageBox.Show("NoData");
                    break;
                case GeoPositionStatus.Initializing:
                    MessageBox.Show("Initializing");
                    break;
            }
        }

        /// <summary>Dispose map explicitly when navigating from the page.
        /// </summary>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
                map.Dispose();
        }

        /// <summary>Indication of map data load status.
        /// </summary>
        private void MapOperationStatusChanged(object sender, OperationStatusChangedEventArgs e)
        {
            switch (e.OperationStatus)
            {
                case OperationStatus.Idle:
                    StatusTextBlock.Text = "Status: Idle";
                    break;
                case OperationStatus.Normal:
                case OperationStatus.Busy:
                    StatusTextBlock.Text = "Status: Busy";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>Update Map's ContentPadding property that specifies map border offset when positioning controls.
        /// </summary>
        private void ContentPanelLayoutUpdated(object sender, EventArgs e)
        {
            map.ContentPadding = new Thickness(24, 24, SecondColumn.ActualWidth + 24, 24);
        }

        /// <summary>Map is scrolled and zoomed to make map rectangle visible.
        /// </summary>
        private void Button4Tap(object sender, GestureEventArgs e)
        {
            map.EnsureControlIsVisible(PushpinArea);
        }

        /// <summary>Map is scrolled and zoomed to make map object visible.
        /// </summary>
        private void Button5Tap(object sender, GestureEventArgs e)
        {
            map.EnsureControlIsVisible(LocationObject);
        }

        private void Button6Tap(object sender, GestureEventArgs e)
        {
            map.EnsureRectangleIsVisible(new GeoCoordinatesRect(56.385114, 36.577315, 55.033086, 38.807161));
        }

        private void MapTap(object sender, GestureEventArgs e)
        {
            Point position = e.GetPosition(map);
            GeoCoordinate coordinates = map.ViewportPointToCoordinates(new Media.Point(position.X, position.Y));
            ProgressIndicator.Text = coordinates.ToHumanReadableString();

            // example of how to determine viewport point by coordinates
            Media.Point point = map.CoordinatesToViewportPoint(coordinates);
            // point is relative to map.Viewport
            int x = Convert.ToInt32(point.X - map.Viewport.X),
                y = Convert.ToInt32(point.Y - map.Viewport.Y);
            //Debug.Assert(x == Convert.ToInt32(position.X) && y == Convert.ToInt32(position.Y));
        }
    }
}