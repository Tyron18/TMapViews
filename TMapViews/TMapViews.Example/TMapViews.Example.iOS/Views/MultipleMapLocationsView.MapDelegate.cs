using MapKit;
using System;
using TMapViews.Example.Core.Models;
using TMapViews.iOS;
using UIKit;

namespace TMapViews.Example.iOS.Views
{
    public partial class MultipleMapLocationsView
    {
        public class ExampleBindingMapDelegate : BindingMKMapViewDelegate
        {
            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            {
                string annotaionIdentifier = "anno";
                if (annotation is BindingMKAnnotation bAnno && bAnno.Annotation is ExampleBindingAnnotation eAnno)
                {
                    MKAnnotationView view = mapView.DequeueReusableAnnotation(bAnno.AnnotationIdentifier);
                    bAnno.SetTitle(eAnno.Id + "");
                    bAnno.SetSubtitle(eAnno.Id + "");
                    if (view == null)
                        view = new MKAnnotationView(bAnno, Guid.NewGuid().ToString());
                    else
                    {
                        view.Annotation = bAnno;
                    }

                    view.CanShowCallout = false;
                    view.Draggable = true;
                    switch (eAnno.Id)
                    {
                        case 1:
                            view.Image = UIImage.FromBundle("Images/marker_a");
                            break;
                        case 2:
                            view.Image = UIImage.FromBundle("Images/marker_b");
                            break;
                        case 3:
                            view.Image = UIImage.FromBundle("Images/marker_c");
                            break;
                        case 4:
                            view.Image = UIImage.FromBundle("Images/marker_d");
                            break;
                        case 5:
                            view.Image = UIImage.FromBundle("Images/marker_e");
                            break;
                    }

                    return view;
                }

                return null;        //Lets the map default behavior take over if the annotation isnt a BindingMKAnnotation.
            }
        }
    }
}
