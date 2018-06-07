using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TMapViews.Models.Interfaces;
using UIKit;

namespace TMapViews.iOS.Models
{
    public interface IBindingMKMapAnnotation
    {
        IBindingMapAnnotation Annotation { get; set; }
    }
}