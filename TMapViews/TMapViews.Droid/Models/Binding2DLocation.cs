using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TMapViews.Models;

namespace TMapViews.Droid.Models
{
    public class Binding2DLocation : I2DLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static Binding2DLocation FromLatLng(LatLng latLng)
            => new Binding2DLocation
            {
                Latitude = latLng.Latitude,
                Longitude = latLng.Longitude
            };

        public LatLng LatLng
            => new LatLng(Latitude, Longitude);
    }
}