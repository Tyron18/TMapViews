using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TMapViews.Droid.Adapters;
using TMapViews.Droid.Models;
using TMapViews.Models;
using TMapViews.Models.Interfaces;
using TMapViews.Models.Models;

namespace TMapViews.Droid.Views
{
    public partial class BindingMapView : MapView,
        IOnMapReadyCallback
    {
        public BindingMapView(Context context) : base(context)
        {
        }

        public BindingMapView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public BindingMapView(Context context, GoogleMapOptions options) : base(context, options)
        {
        }

        public BindingMapView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        protected BindingMapView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public new void OnResume()
        {
            if (IsReady)
                base.OnResume();
        }

        public void GetMapAsync(IBindingMapAdapter adapter = null, Bundle savedInstanceState = null)
        {
            Adapter = adapter;
            OnCreate(savedInstanceState);
            base.GetMapAsync(this);
            OnResume();
        }

        /// <summary>
        /// Attempts to initialize maps.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Returns a result code from Android.Gms.Common.ResultCode where a 0 is a success.</returns>
        public int Initialize(Activity context, IBindingMapAdapter adapter = null, Bundle savedInstanceState = null)
        {
            MapsInitializer.Initialize(context);
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(context);

            if (resultCode == ConnectionResult.Success)
            {
                GetMapAsync(adapter, savedInstanceState);
            }
            return resultCode;
        }

        public GoogleMap GoogleMap { get; private set; }
        public IBindingMapAdapter Adapter { get; set; }

        private int _mapType = 1;

        public int MapType
        {
            get => _mapType;
            set
            {
                _mapType = value;
                if (GoogleMap != null)
                    GoogleMap.MapType = _mapType;
            }
        }

        private bool _showUserLocation;

        public bool MyLocationEnabled
        {
            get => _showUserLocation;
            set
            {
                _showUserLocation = value;
                if (GoogleMap != null)
                    GoogleMap.MyLocationEnabled = _showUserLocation;
            }
        }

        private bool _rotateGesturesEnabled;

        public bool RotateGesturesEnabled
        {
            get => _rotateGesturesEnabled;
            set
            {
                _rotateGesturesEnabled = value;
                if (GoogleMap != null)
                    GoogleMap.UiSettings.RotateGesturesEnabled = _rotateGesturesEnabled;
            }
        }

        private bool _tiltGesturesEnabled;

        public bool TiltGesturesEnabled
        {
            get => _tiltGesturesEnabled;
            set
            {
                _tiltGesturesEnabled = value;
                if (GoogleMap != null)
                    GoogleMap.UiSettings.TiltGesturesEnabled = _tiltGesturesEnabled;
            }
        }

        private bool _scrollGesturesEnabled;

        public bool ScrollGesturesEnabled
        {
            get => _scrollGesturesEnabled;
            set
            {
                _scrollGesturesEnabled = value;
                if (GoogleMap != null)
                    GoogleMap.UiSettings.TiltGesturesEnabled = _tiltGesturesEnabled;
            }
        }

        private bool _zoomControlsEnabled;

        public bool ZoomControlsEnabled
        {
            get => _zoomControlsEnabled;
            set
            {
                _zoomControlsEnabled = value;
                if (GoogleMap != null)
                    GoogleMap.UiSettings.ZoomControlsEnabled = _zoomControlsEnabled;
            }
        }

        private bool _zoomGesturesEnabled;

        public bool ZoomGesturesEnabled
        {
            get => _zoomGesturesEnabled;
            set
            {
                _zoomGesturesEnabled = value;
                if (GoogleMap != null)
                    GoogleMap.UiSettings.ZoomGesturesEnabled = _zoomGesturesEnabled;
            }
        }

        private bool _myLocationButtonEnabled;

        public bool MyLocationButtonEnabled
        {
            get => _myLocationButtonEnabled;
            set
            {
                _myLocationButtonEnabled = value;
                if (GoogleMap != null)
                    GoogleMap.UiSettings.MyLocationButtonEnabled = _myLocationButtonEnabled;
            }
        }

        private bool _annotationsVisible = true;

        public bool AnnotationsVisible
        {
            get => _annotationsVisible;
            set
            {
                _annotationsVisible = value;
                UpdateAnnotations();
            }
        }

        private Binding2DLocation _centerMapLocation;

        public Binding2DLocation CenterMapLocation
        {
            get => _centerMapLocation;
            set
            {
                _centerMapLocation = value;
                CenterOn(_centerMapLocation);
            }
        }

        public float Zoom { get; set; } = 17f;

        private void CenterOn(Binding2DLocation centerMapLocation, float? zoom = null) => GoogleMap?.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(centerMapLocation.ToLatLng(), zoom ?? Zoom));

        public bool IsReady { get; set; }

        private IEnumerable<IBindingMapAnnotation> _annotationSource;

        public IEnumerable<IBindingMapAnnotation> AnnotationSource
        {
            get => _annotationSource;
            set
            {
                _annotationSource = value;
                UpdateAnnotations();
            }
        }

        private void UpdateAnnotations()
        {
            GoogleMap?.Clear();
            if (AnnotationsVisible
                && Adapter != null
                && AnnotationSource != null)
            {
                foreach (var annotation in AnnotationSource)
                {
                    if (annotation is IBindingMapOverlay mOverlay)
                    {
                        var overlayOptions = Adapter.AddBindingMapOverlay(GoogleMap, mOverlay);
                        var overlay = AddOverlay(overlayOptions);
                        if (overlay is Circle circle)
                            circle.Tag = new AnnotationTag
                            {
                                Annotation = mOverlay
                            };
                        else if (overlay is Polygon polygon)
                            polygon.Tag = new AnnotationTag
                            {
                                Annotation = mOverlay
                            };
                        else if (overlay is Polyline polyLine)
                            polyLine.Tag = new AnnotationTag
                            {
                                Annotation = mOverlay
                            };
                        else if (overlay is GroundOverlay groundOverlay)
                            groundOverlay.Tag  = new AnnotationTag
                            {
                                Annotation = mOverlay
                            };
                        else
                            throw new OverlayAdapterException(overlay);
                    }
                    else if (annotation is IBindingMapAnnotation mMarker)
                    {
                        var marker = GoogleMap.AddMarker(Adapter.GetMarkerOptionsForPin(annotation));
                        marker.Tag = new AnnotationTag
                        {
                            Annotation = annotation
                        };
                    }

                }
            }
        }

        private IJavaObject AddOverlay(IJavaObject overlayOptions)
        {
            if (overlayOptions is CircleOptions circle)
                return GoogleMap.AddCircle(circle);
            if (overlayOptions is PolygonOptions poly)
                return GoogleMap.AddPolygon(poly);
            if (overlayOptions is PolylineOptions line)
                return GoogleMap.AddPolyline(line);
            if (overlayOptions is GroundOverlayOptions gOverlay)
                return GoogleMap.AddGroundOverlay(gOverlay);
            if (overlayOptions is TileOverlayOptions tOverlay)
                return GoogleMap.AddTileOverlay(tOverlay);
            throw new OverlayAdapterException(overlayOptions);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            GoogleMap = googleMap;
            UpdateUiSettings();
            IsReady = true;
            GoogleMap.MyLocationChange += OnLocationChanged;
            OnResume();
            if (CenterMapLocation != null)
                CenterOn(CenterMapLocation);
            SetListeners();
            UpdateAnnotations();
        }

        private void UpdateUiSettings()
        {
            if (GoogleMap != null)
            {
                GoogleMap.UiSettings.MyLocationButtonEnabled = MyLocationButtonEnabled;
                GoogleMap.MyLocationEnabled = MyLocationEnabled;

                GoogleMap.UiSettings.RotateGesturesEnabled = RotateGesturesEnabled;
                GoogleMap.UiSettings.TiltGesturesEnabled = TiltGesturesEnabled;
                GoogleMap.UiSettings.ScrollGesturesEnabled = ScrollGesturesEnabled;
                GoogleMap.UiSettings.ZoomControlsEnabled = ZoomControlsEnabled;
                GoogleMap.UiSettings.ZoomGesturesEnabled = ZoomGesturesEnabled;
            }
        }
    }
}