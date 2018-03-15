using Android.Locations;
using Android.Runtime;
using System;
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