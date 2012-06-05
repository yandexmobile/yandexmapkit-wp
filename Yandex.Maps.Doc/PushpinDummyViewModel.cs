using System.Windows;
using Yandex.Positioning;

namespace Yandex.Maps.Doc
{
    public class PushpinDummyViewModel
    {
        public Visibility Visibility { get; set; }
        public Visibility ContentVisibility { get; set; }
        public PushPinState State { get; set; }
        public GeoCoordinate Position { get; set; }
        public int ZIndex { get; set; }
    }
}
