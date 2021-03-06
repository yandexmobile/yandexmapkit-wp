﻿namespace Yandex.Maps.Doc
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            DataContext = this;
        }

        /// <summary>Use this property for enabling or disabling location service globally for all instances of Yandex.Map.
        /// </summary>
        public bool UseLocation
        {
            get
            {
                return MapGlobalSettings.Instance.EnableLocationService;
            }
            set
            {
                MapGlobalSettings.Instance.EnableLocationService = value;
            }
        }
    }
}