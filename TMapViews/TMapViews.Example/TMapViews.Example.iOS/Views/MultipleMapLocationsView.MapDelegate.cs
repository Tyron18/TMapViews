using MapKit;
using ObjCRuntime;
using System;
using TMapViews.Example.Core.Models;
using TMapViews.iOS;
using TMapViews.iOS.Models;
using TMapViews.Models.Interfaces;
using UIKit;

namespace TMapViews.Example.iOS.Views
{
    public partial class MultipleMapLocationsView
    {
        public class ExampleBindingMapDelegate : BindingMKMapViewDelegate
        {
            public override MKAnnotationView GetViewForBindingAnnotation(MKMapView mapView, IBindingMapAnnotation bindingMapAnnotation)
            {
                if (bindingMapAnnotation is ExampleBindingAnnotation eAnno)
                {
                    MKAnnotationView view = mapView.DequeueReusableAnnotation(eAnno.Id + "");
                    var annotation = new BindingMKAnnotation(bindingMapAnnotation);
                    annotation.SetTitle(eAnno.Id + "");
                    annotation.SetSubtitle(eAnno.Id + "");
                    if (view == null)
                        view = new MKAnnotationView(annotation, eAnno.Id + "");
                    else
                    {
                        view.Annotation = annotation;
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

            public override IBindingMKMapOverlay GetViewForBindingOverlay(MKMapView mapView, IBindingMapOverlay bindingMapOverlay)
            {
                if (bindingMapOverlay is ExampleBindingOverlay eOverlay)
                {
                    var result = BindingMKCircle.Circle(eOverlay.Location.ToCLLocationCoordinate2D(), eOverlay.Radius);
                    result.Renderer = new MKCircleRenderer(result)
                    {
                        StrokeColor = UIColor.Blue,
                        LineWidth = 1f,
                        FillColor = UIColor.Gray
                    };
                    return result;
                }
                return base.GetViewForBindingOverlay(mapView, bindingMapOverlay);
            }
        }
    }
}