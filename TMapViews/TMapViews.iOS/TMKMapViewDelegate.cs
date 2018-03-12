using MapKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UIKit;

namespace TMapViews.iOS
{
    public class TMKMapViewDelegate : MKMapViewDelegate
    {
        public ICommand LocationChanged { get; set; }

        public override void DidUpdateUserLocation(MKMapView mapView, MKUserLocation userLocation)
        {
            if (LocationChanged != null)
            {
                var coordinate = userLocation.Location.ToTLocation();
                if (LocationChanged.CanExecute(coordinate))
                    LocationChanged.Equals(coordinate);
            }
        }
    }
}
