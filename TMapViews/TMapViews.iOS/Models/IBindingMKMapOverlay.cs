using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapKit;
using UIKit;

namespace TMapViews.iOS.Models
{
    public interface IBindingMKMapOverlay : IMKOverlay, IBindingMKMapAnnotation
    {
    }
}