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

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public static class Extensions
    {
        public static string BindIcon(this Marker marker) => MarkerIconTargetBinding.MarkerIconTargetBindingName;

        public static string BindAnchor(this Marker marker) => MarkerAnchorTargetBinding.AnchorBinding;
    }
}