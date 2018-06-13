using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMapViews.Droid.Adapters;
using TMapViews.Droid.Models;
using TMapViews.Models;

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
            if (adapter != null)
                Adapter = adapter;
            OnCreate(savedInstanceState);
            base.GetMapAsync(this);
            OnResume();
        }

        /// <summary>
        /// Attempts to initialize maps.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>
        /// Returns a result code from Android.Gms.Common.ResultCode where a 0 is
        /// a success.
        /// </returns>
        public int Initialize(Activity context, IBindingMapAdapter adapter, Bundle savedInstanceState = null)
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

        public IBindingMapAdapter Adapter
        {
            get;
            private set;
        }

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

        private I2DLocation _centerMapLocation;

        public I2DLocation CenterMapLocation
        {
            get => _centerMapLocation;
            set
            {
                _centerMapLocation = value;
                CenterOn(_centerMapLocation);
            }
        }

        private I3DLocation _userLocation;

        public I3DLocation UserLocation
        {
            get => _userLocation;
            protected set
            {
                _userLocation = value;
                UserLocationChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// WARNING :: Do not use this event handler. Rather use the
        /// LocationChanged Command.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<I3DLocation> UserLocationChanged;

        public float Zoom { get; set; } = 0.2f;

        private void CenterOn(I2DLocation centerMapLocation, float? zoom = null) => GoogleMap?.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(centerMapLocation.ToLatLng(), GetGoogleMapZoom(zoom ?? Zoom)));

        private float GetGoogleMapZoom(float degrees)
        {
            DisplayMetrics metrics = new DisplayMetrics();
            if (((Activity)Context).WindowManager?.DefaultDisplay != null)
                ((Activity)Context).WindowManager?.DefaultDisplay.GetMetrics(metrics);
            else
                metrics = null;

            double dp = 410;
            if (metrics != null)
            {
                var shortestMeasure = metrics.WidthPixels > metrics.HeightPixels ? metrics.HeightPixels : metrics.WidthPixels;
                dp = shortestMeasure / metrics.Density;
            }
            var x = (float)System.Math.Log((dp * 45) / (degrees * 32), 2);
            return x;
        }

        public bool IsReady { get; private set; }

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

        private IEnumerable<IBindingMapOverlay> _overlaySource;

        public IEnumerable<IBindingMapOverlay> OverlaySource
        {
            get => _overlaySource;
            set
            {
                _overlaySource = value;
                UpdateAnnotations();
            }
        }

        private void UpdateAnnotations()
        {
            GoogleMap?.Clear();
            if (AnnotationsVisible
                && Adapter != null
                && IsReady)
            {
                if (AnnotationSource != null)
                    foreach (var annotation in AnnotationSource)
                    {
                        if (annotation is IBindingMapAnnotation mMarker)
                        {
                            var markerOptions = Adapter.GetMarkerOptionsForPin(annotation);
                            if (markerOptions != null)
                            {
                                var marker = GoogleMap.AddMarker(markerOptions);
                                marker.Tag = new AnnotationTag
                                {
                                    Annotation = annotation
                                };
                            }
                        }
                    }

                if (OverlaySource != null)
                    foreach (var overlay in OverlaySource)
                    {
                        if (overlay is IBindingMapOverlay mOverlay)
                        {
                            var overlayOptions = Adapter.AddBindingMapOverlay(GoogleMap, mOverlay);
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