using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MapKit;
using TMapViews.Example.Core.Models;
using TMapViews.iOS;
using UIKit;

namespace TMapViews.Example.iOS.Views
{
    public partial class LocationTrackingView
    {
        public class LocationTrackingMapDelegate : BindingMKMapViewDelegate
        {
            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            {
                if (annotation is BindingMKAnnotation bAnno && bAnno.Annotation is ExampleBindingAnnotation eAnno )
                {
                    var view = mapView.DequeueReusableAnnotation(bAnno.AnnotationIdentifier);
                    bAnno.SetTitle(eAnno.Id + "");
                    bAnno.SetSubtitle(eAnno.Id + "");
                    if (view == null)
                        view = new MKAnnotationView(bAnno, Guid.NewGuid().ToString());
                    else
                    {
                        view.Annotation = bAnno;
                    }
                    view.Image = UIImage.FromBundle("Images/sphere");
                    return view;
                }

                return null;
            }
        }
    }
}