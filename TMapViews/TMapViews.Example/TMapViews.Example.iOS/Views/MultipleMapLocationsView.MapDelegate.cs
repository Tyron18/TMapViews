﻿using CoreGraphics;
using MapKit;
using MvvmCross.Binding.BindingContext;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using TMapViews.Example.Core.Models;
using TMapViews.iOS;
using TMapViews.iOS.Models;
using TMapViews.Models;
using TMapViews.MvxPlugins.Bindings.iOS;
using UIKit;

namespace TMapViews.Example.iOS.Views
{
    public partial class MultipleMapLocationsView
    {
        public class ExampleBindingMapDelegate : MvxBindingMkMapViewDelegate
        {
            public override MvxBindingMKAnnotationView GetViewForBindingAnnotation(MKMapView mapView)
            {
                var result = mapView.DequeueReusableAnnotation("Example") as ExamplePinMvxBindingAnnotationView;
                if (result == null)
                    result = new ExamplePinMvxBindingAnnotationView("Example");
                return result;
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

        public class ExamplePinMvxBindingAnnotationView : MvxBindingMKAnnotationView
        {
            private nfloat _imageHeight;

            public ExamplePinMvxBindingAnnotationView(string reuseIdentifier) : base(reuseIdentifier)
            {
                _imageHeight = UIImage.FromBundle("Images/marker_a").Size.Height;
                this.DelayBind(() =>
                {
                    var bindingSet = this.CreateBindingSet<ExamplePinMvxBindingAnnotationView, ExampleBindingAnnotation>();
                    bindingSet.Bind(this).For(v => v.Image).To(vm => vm.Id).WithDictionaryConversion(new Dictionary<int, UIImage>
                    {
                        {1, UIImage.FromBundle("Images/marker_a")},
                        {2, UIImage.FromBundle("Images/marker_b")},
                        {3, UIImage.FromBundle("Images/marker_c")},
                        {4, UIImage.FromBundle("Images/marker_d")},
                        {5, UIImage.FromBundle("Images/marker_e")}
                    });
                    bindingSet.Bind(this).For(v => v.BindScale()).To(vm => vm.Selected).WithDictionaryConversion(new Dictionary<bool, nfloat>
                    {
                        {true, 1.3f},
                        {false, 1/1.3f}
                    });
                    bindingSet.Bind(this).For(v => v.CenterOffset).To(vm => vm.Selected).WithDictionaryConversion(new Dictionary<bool, CGPoint>
                    {
                        {true,  CenterOffset = new CGPoint(0, -(_imageHeight * 1.3 / 2))},
                        {false,  CenterOffset = new CGPoint(0, -(_imageHeight / 2))}
                });
                    bindingSet.Apply();
                });
            }

            public override void OnAnnotationSet()
            {
                Annotation.SetTitle("title");
                Annotation.SetSubtitle("subTitle");
            }
        }
    }
}