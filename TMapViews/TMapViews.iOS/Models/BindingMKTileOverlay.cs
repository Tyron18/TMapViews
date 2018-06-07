using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using CoreLocation;
using Foundation;
using MapKit;
using TMapViews.Models.Interfaces;
using UIKit;

namespace TMapViews.iOS.Models
{
    public class BindingMKTileOverlay : MKTileOverlay, IBindingMKMapOverlay
    {
        public IBindingMapAnnotation Annotation { get; set; }
        public MKOverlayRenderer Renderer { get; set; }

        public BindingMKTileOverlay(string URLTemplate) : base(URLTemplate)
        {
        }
    }
}