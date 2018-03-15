using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TMapViews.Models.Interfaces;

namespace TMapViews.Droid.Models
{
    public class Binding3DLocation : Location, I3DLocation
    {
        public Binding3DLocation(Location l) : base(l)
        {
        }

        public Binding3DLocation(string provider) : base(provider)
        {
        }

        protected Binding3DLocation(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public double HorizontalAccuracy => base.Accuracy;

        public double VerticalAccuracy => base.VerticalAccuracyMeters;

        double I3DLocation.Speed => base.Speed;
    }
}