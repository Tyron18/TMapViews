using System;
using System.Collections.Generic;
using Cirrious.FluentLayouts.Touch;
using MapKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TMapViews.Example.Core.ViewModels;
using TMapViews.iOS;
using UIKit;

namespace TMapViews.Example.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController =true)]
    public class MultipleMapLocationsView : MvxViewController<MultipleMapLocationsViewModel>
    {
        ExampleBindingMapDelegate _mapDelegate;
        BindingMKMapView _mapView;
        UIView _info;
        UITextView _latitude, _longitude, _colon;
        FluentLayout _hiddenInfo;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _mapDelegate = new ExampleBindingMapDelegate();

            InitializeSubviews();
            LayoutSubviews();
            DoBindings();
        }

        private void InitializeSubviews()
        {
            _mapView = new BindingMKMapView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                ScrollEnabled = false,
                UserInteractionEnabled = true,
                Delegate = _mapDelegate,
                ZoomLevel = 72.25
            };

            _info = new UIView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            _latitude = new UITextView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                TextAlignment = UITextAlignment.Right
            };

            _longitude = new UITextView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            _colon = new UITextView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Text = ":",
            };

            View.AddSubviews(_mapView, _info);
            _info.AddSubviews(_latitude, _longitude, _colon);
        }
        private void LayoutSubviews()
        {
            _hiddenInfo = _info.Height().EqualTo(0f);

            View.AddConstraints(_mapView.FullHeightOf(View));
            View.AddConstraints(_mapView.FullWidthOf(View));
            View.AddConstraints(new FluentLayout[]
            {
                _hiddenInfo,
                _info.AtBottomOf(View),
                _info.AtLeftOf(View),
                _info.AtRightOf(View)
            });

            _info.AddConstraints(new FluentLayout[]
            {
                _colon.WithSameCenterX(_info),
                _colon.AtBottomOf(_info,10f),
                _colon.AtTopOf(_info,10f),

                _latitude.AtLeftOf(_info, 10f),
                _latitude.ToRightOf(_colon, 10f),
                _latitude.AtBottomOf(_info, 10f),
                _latitude.AtTopOf(_info, 10f),

                _longitude.AtRightOf(_info, 10f),
                _longitude.ToLeftOf(_colon, 10f),
                _longitude.AtBottomOf(_info, 10f),
                _longitude.AtTopOf(_info, 10f),
            });
        }

        private void DoBindings()
        {
            var bindingSet = this.CreateBindingSet<MultipleMapLocationsView, MultipleMapLocationsViewModel>();
            bindingSet.Bind(_mapView).For(v => v.CenterMapLocation).To(vm => vm.Center);
            bindingSet.Bind(_mapView).For(v => v.AnnotationSource).To(vm => vm.Pins);
            bindingSet.Bind(_mapDelegate).For(v => v.MarkerClick).To(vm => vm.MarkerTappedCommand);
            bindingSet.Bind(_mapDelegate).For(v => v.OverlayClicked).To(vm => vm.MarkerTappedCommand);
            bindingSet.Bind(_mapDelegate).For(v => v.MarkerDragStart).To(vm => vm.MarkerDragStartCommand);
            bindingSet.Bind(_mapDelegate).For(v => v.MarkerDragEnd).To(vm => vm.MarkerDragEndCommand);
            bindingSet.Bind(_mapDelegate).For(v => v.MarkerDrag).To(vm => vm.MarkerDragCommand);
            bindingSet.Bind(_info).For(v => v.Hidden).To(vm => vm.Dragging).WithDictionaryConversion
                (
                    new Dictionary<bool, bool>
                    {
                        {true, false },
                        {false, true }
                    }
                );
            bindingSet.Bind(_hiddenInfo).For(v => v.Active).To(vm => vm.Dragging).WithDictionaryConversion
                (
                    new Dictionary<bool, bool>
                    {
                        {true, false },
                        {false, true }
                    }
                );
            //bindingSet.Bind(_latitude).For(v => v.Text).To(vm => vm.Latitude).WithConversion<ParseDoubleValueConverter>();
            //bindingSet.Bind(_longitude).For(v => v.Text).To(vm => vm.Longitude).WithConversion<ParseDoubleValueConverter>();
            bindingSet.Apply();
        }
    }

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
