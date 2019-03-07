using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Runtime;
using System.Collections.Generic;
using System.Linq;
using TMapViews.Droid.Models;
using TMapViews.Droid.Views;
using TMapViews.Models;

namespace TMapViews.Droid.Adapters
{
    public abstract class BindingMapAdapter
    {
        public BindingMapAdapter(BindingMapView mapView)
        {
            _mapView = mapView;
        }

        public abstract IJavaObject AddBindingMapOverlay(IBindingMapOverlay overlay);

        public abstract MarkerOptions GetMarkerOptionsForPin(IBindingMapAnnotation pin);


        private IEnumerable<IBindingMapAnnotation> _annotationSource;

        public virtual IEnumerable<IBindingMapAnnotation> AnnotationSource
        {
            get => _annotationSource;
            set
            {
                _annotationSource = value;
                UpdateAnnotations();
            }
        }

        private IEnumerable<IBindingMapOverlay> _overlaySource;
        protected List<Marker> _markers;
        private BindingMapView _mapView;
        public BindingMapView MapView => _mapView;

        public virtual IEnumerable<IBindingMapOverlay> OverlaySource
        {
            get => _overlaySource;
            set
            {
                _overlaySource = value;
                UpdateAnnotations();
            }
        }

        public virtual void UpdateAnnotations()
        {
            _mapView.GoogleMap?.Clear();
            if (_mapView.AnnotationsVisible
                && _mapView.IsReady)
            {
                if (AnnotationSource != null)
                {
                    int i = 0;
                    while (i < AnnotationSource.Count())
                    {
                        var annotation = AnnotationSource.ElementAt(i++);
                        AddAnnotation(annotation);
                    }
                }

                if (OverlaySource != null)
                {
                    int i = 0;
                    while (i < OverlaySource.Count())
                    {
                        var overlay = OverlaySource.ElementAt(i++);
                        if (overlay is IBindingMapOverlay mOverlay)
                        {
                            var overlayOptions = AddBindingMapOverlay(mOverlay);
                            if (overlayOptions != null)
                            {
                                var overlayView = AddOverlay(overlayOptions);
                                if (overlayView is Circle circle)
                                    circle.Tag = new AnnotationTag
                                    {
                                        Annotation = mOverlay
                                    };
                                else if (overlayView is Polygon polygon)
                                    polygon.Tag = new AnnotationTag
                                    {
                                        Annotation = mOverlay
                                    };
                                else if (overlayView is Polyline polyLine)
                                    polyLine.Tag = new AnnotationTag
                                    {
                                        Annotation = mOverlay
                                    };
                                else if (overlayView is GroundOverlay groundOverlay)
                                    groundOverlay.Tag = new AnnotationTag
                                    {
                                        Annotation = mOverlay
                                    };
                                else
                                    throw new OverlayAdapterException(overlayView);
                            }
                        }
                    }
                }
            }
        }

        public virtual void AddAnnotation(IBindingMapAnnotation annotation)
        {
            if (annotation is IBindingMapAnnotation mMarker)
            {
                var markerOptions = GetMarkerOptionsForPin(annotation);
                if (markerOptions != null)
                {
                    var marker = _mapView.GoogleMap.AddMarker(markerOptions);
                    marker.Tag = new AnnotationTag
                    {
                        Annotation = annotation
                    };
                    if (_markers == null)
                        _markers = new List<Marker>();
                    _markers.Add(marker);
                }
            }
        }


        public virtual void RemoveAnnotation(IBindingMapAnnotation item)
        {
            var marker = _markers.SingleOrDefault(x => ReferenceEquals((x.Tag as AnnotationTag)?.Annotation, item));
            if (marker != null)
            {
                marker.Remove();
                _markers.Remove(marker);
                marker.Dispose();
                marker = null;
            }
        }

        protected virtual IJavaObject AddOverlay(IJavaObject overlayOptions)
        {
            if (overlayOptions is CircleOptions circle)
                return _mapView.GoogleMap.AddCircle(circle);
            if (overlayOptions is PolygonOptions poly)
                return _mapView.GoogleMap.AddPolygon(poly);
            if (overlayOptions is PolylineOptions line)
                return _mapView.GoogleMap.AddPolyline(line);
            if (overlayOptions is GroundOverlayOptions gOverlay)
                return _mapView.GoogleMap.AddGroundOverlay(gOverlay);
            if (overlayOptions is TileOverlayOptions tOverlay)
                return _mapView.GoogleMap.AddTileOverlay(tOverlay);
            throw new OverlayAdapterException(overlayOptions);
        }

    }
}