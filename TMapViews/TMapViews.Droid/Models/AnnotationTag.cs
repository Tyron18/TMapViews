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
using TMapViews.Models.Interfaces;

namespace TMapViews.Droid.Models
{
    public class AnnotationTag : Java.Lang.Object
    {
        public IBindingMapAnnotation Annotation { get; set; }
    }
}