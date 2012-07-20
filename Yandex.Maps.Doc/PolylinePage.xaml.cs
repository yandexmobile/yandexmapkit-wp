using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using Yandex.Positioning;

namespace Yandex.Maps.Doc
{
    /// <summary><see cref="Yandex.Maps.MapPolyline"/> control demonstration.
    /// </summary>
    public partial class PolylinePage
    {
        private readonly Random _random;

        public PolylinePage()
        {
            // set current culture to force map to show localized data
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

            InitializeComponent();

            Locations = new ObservableCollection<GeoCoordinate>();

            Locations.Add(new GeoCoordinate(53.92, 27.55));
            Locations.Add(new GeoCoordinate(53.91, 27.56));
            Locations.Add(new GeoCoordinate(53.90, 27.56));
            Locations.Add(new GeoCoordinate(53.91, 27.56));
            Locations.Add(new GeoCoordinate(53.92, 27.57));

            AddLocations = new ActionCommand(AddLocationsHandler);

            DataContext = this;

            _random = new Random();
        }

        public ObservableCollection<GeoCoordinate> Locations { get; set; }

        public ICommand AddLocations { get; set; }

        private void AddLocationsHandler()
        {
            Locations.Add(
                new GeoCoordinate(
                    _random.NextDouble() * 0.1 + 53.92, 
                    _random.NextDouble() * 0.2 + 27.55));
        }
    }
}