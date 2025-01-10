using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GogoRCustomerManager
{
    class GosafeMap
    {
        public GMapControl App;

        public GosafeMap(GMapControl app)
        {
            App = app;
            App.MapProvider = GMapProviders.GoogleMap;

            App.Zoom = 15; 
            App.MinZoom = 1;
            App.MaxZoom = 25;
            // Default Position
            App.Position = new PointLatLng(37.497872, 127.0275142);
        }

        public PointLatLng Position
        {
            get { return App.Position; }
            set { App.Position = value; }
        }

        public void SetPositionByKeywords(string keys)
        {
            App.SetPositionByKeywords(keys);
        }
    }
}
