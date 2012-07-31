using System;
using System.Linq;
using Microsoft.Phone.Controls;

namespace Yandex.Maps.Doc
{
    public partial class SchemaPage : PhoneApplicationPage
    {
        // Constructor
        public SchemaPage()
        {
            InitializeComponent();
        }

        private void ZoomInClick(object sender, EventArgs e)
        {
            map.ZoomIn();
        }

        private void ZoomOutClick(object sender, EventArgs e)
        {
            map.ZoomOut();
        }

        private void EnsureVisibilityClick(object sender, EventArgs e)
        {
            map.EnsureVisibility(schemaLayer.Children.ToArray());
        }
    }
}