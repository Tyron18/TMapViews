using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TMapViews.Models;

namespace TMapViews.Droid.Models
{
    public abstract class BindingMapMarker : Java.Lang.Object, IBindingMapAnnotation
    {
        public abstract I2DLocation Location { get; set; }
    }
}