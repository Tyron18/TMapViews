using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapKit;
using TMapViews.Example.Core.Models;
using TMapViews.iOS;
using TMapViews.Models.Interfaces;
using UIKit;

namespace TMapViews.Example.iOS.Views
{
    public partial class LocationTrackingView
    {
        public class LocationTrackingMapDelegate : BindingMKMapViewDelegate
        {
            public override MKAnnotationView GetViewForBindingAnnotation(MKMapView mapView, IBindingMapAnnotation bindingMapAnnotation)
            {
                if (bindingMapAnnotation is ExampleBindingAnnotation eAnno)
                {
                    var view = mapView.DequeueReusableAnnotation(eAnno.Id + "");
                    var annotation = new BindingMKAnnotation(bindingMapAnnotation);
                    annotation.SetTitle(eAnno.Id + "");
                    annotation.SetSubtitle(eAnno.Id + "");
                    if (view == null)
                        view = new MKAnnotationView(annotation, eAnno.Id + "");
                    else
                    {
                        view.Annotation = annotation;
                    }
                    view.Image = UIImage.FromBundle("Images/sphere");
                    return view;
                }

                return null;
            }
        }
    }
}