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
using MvvmCross.Binding;
using MvvmCross.Platforms.Android.Binding.Target;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public class MarkerIconTargetBinding : MvxAndroidTargetBinding<Marker, BitmapDescriptor>
    {
        public static string MarkerIconTargetBindingName => "BindIcon";

        public MarkerIconTargetBinding(Marker target) : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValueImpl(Marker target, BitmapDescriptor value)
        {
            target.SetIcon(value);
        }
    }
}