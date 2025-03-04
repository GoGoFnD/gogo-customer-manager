using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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
        public GMapOverlay markerOverlay = new GMapOverlay("markers");
        public GMapOverlay selectedMarkOverlay = new GMapOverlay("SelectedMark");
        public List<PointLatLng> AllMarkers = new List<PointLatLng>(); // 모든 마커의 위치 저장
        public GosafeMap(GMapControl app)
        {
            App = app;
            App.MapProvider = GMapProviders.GoogleMap;

            App.Zoom = 15; 
            App.MinZoom = 1;
            App.MaxZoom = 25;
            // Default Position
            App.Position = new PointLatLng(37.497872, 127.0275142);

            App.OnMapDrag += UpdateVisibleMarkers;
            App.OnMapZoomChanged += UpdateVisibleMarkers;
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
        public void AddMarker(double lat, double lng)
        {
            var point = new PointLatLng(lat, lng);
            AllMarkers.Add(point); // 마커 위치를 리스트에 추가
            //var marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.red_dot);
            //markerOverlay.Markers.Add(marker);
            //App.Overlays.Add(markerOverlay);
            //App.Overlays.Add(markerOverlay);
            UpdateVisibleMarkers();
        }
        public void AddSelectedMarker(double lat, double lng)
        {
            selectedMarkOverlay.Markers.Clear();
            App.Overlays.Clear();
            var point = new PointLatLng(lat, lng);
            AllMarkers.Add(point); // 마커 위치를 리스트에 추가
            var marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.blue_dot);
            selectedMarkOverlay.Markers.Add(marker);
            App.Overlays.Add(selectedMarkOverlay);

        }
        public void RemoveMarkers()
        {
            Console.WriteLine("qowe");
            markerOverlay.Markers.Clear();
            AllMarkers.Clear();
            App.Refresh();
        }
        private void UpdateVisibleMarkers()
        {
            var bounds = App.ViewArea; // 현재 보이는 영역
            markerOverlay.Markers.Clear(); // 기존 마커 초기화

            foreach (var point in AllMarkers)
            {
                if (bounds.Contains(point)) // 보이는 영역에 포함된 마커만 추가
                {
                    var marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
                    markerOverlay.Markers.Add(marker);
                }
            }

            if (!App.Overlays.Contains(markerOverlay))
            {
                App.Overlays.Add(markerOverlay);
            }

            App.Refresh();
        }

        public void AddClusteredMarkers()
        {
            int clusterRadius = 50; // 클러스터 반경 (픽셀 단위)
            var clusters = new List<List<PointLatLng>>();

            foreach (var point in AllMarkers)
            {
                bool addedToCluster = false;

                foreach (var cluster in clusters)
                {
                    if (cluster.Any(p => CalculateDistance(p, point) < clusterRadius))
                    {
                        cluster.Add(point);
                        addedToCluster = true;
                        break;
                    }
                }

                if (!addedToCluster)
                {
                    clusters.Add(new List<PointLatLng> { point });
                }
            }

            markerOverlay.Markers.Clear();

            foreach (var cluster in clusters)
            {
                if (cluster.Count == 1)
                {
                    var marker = new GMarkerGoogle(cluster[0], GMarkerGoogleType.red_dot);
                    markerOverlay.Markers.Add(marker);
                }
                else
                {
                    var center = new PointLatLng(
                        cluster.Average(p => p.Lat),
                        cluster.Average(p => p.Lng)
                    );
                    var marker = new GMarkerGoogle(center, GMarkerGoogleType.blue_dot);
                    marker.ToolTipText = $"{cluster.Count} markers here";
                    markerOverlay.Markers.Add(marker);
                }
            }

            if (!App.Overlays.Contains(markerOverlay))
            {
                App.Overlays.Add(markerOverlay);
            }

            App.Refresh();
        }
        private double CalculateDistance(PointLatLng point1, PointLatLng point2)
        {
            var R = 6371e3; // 지구 반경 (미터)
            var lat1 = point1.Lat * Math.PI / 180;
            var lat2 = point2.Lat * Math.PI / 180;
            var deltaLat = (point2.Lat - point1.Lat) * Math.PI / 180;
            var deltaLng = (point2.Lng - point1.Lng) * Math.PI / 180;

            var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Sin(deltaLng / 2) * Math.Sin(deltaLng / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // 결과 거리 (미터)
        }
    }
}
