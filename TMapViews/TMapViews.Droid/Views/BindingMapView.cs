using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Util;
using System;
using System.Collections.ObjectModel;
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

        public void GetMapAsync(IBindingMapAdapter adapter, Bundle savedInstanceState = null)
        {
            Adapter = adapter;
            OnCreate(savedInstanceState);
            base.GetMapAsync(this);
            OnResume();
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

        private bool _annotationsVisible;

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

        private void CenterOn(Binding2DLocation centerMapLocation, float? zoom = null) => GoogleMap?.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(centerMapLocation.LatLng, zoom ?? Zoom));

        public bool IsReady { get; set; }

        private ObservableCollection<IBindingMapAnnotation> _annotationSource;

        public ObservableCollection<IBindingMapAnnotation> AnnotationSource
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
                    if (annotation is BindingMapMarker mMarker)
                    {
                        var marker = GoogleMap.AddMarker(Adapter.GetMarkerOptionsForPin(mMarker));
                        marker.Tag = mMarker;
                    }
                    else if (annotation is BindingMapOverlay mOverlay)
                    {
                        var overlay = Adapter.AddBindingMapOverlay(GoogleMap, annotation);
                        if (overlay is Circle circle)
                            circle.Tag = mOverlay;
                        else if (overlay is Polygon polygon)
                            polygon.Tag = mOverlay;
                        else if (overlay is Polyline polyLine)
                            polyLine.Tag = mOverlay;
                        else if (overlay is GroundOverlay groundOverlay)
                            groundOverlay.Tag = mOverlay;
                        else
                            throw new OverlayAdapterException(overlay);
                    }
                }
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            GoogleMap = googleMap;
            UpdateUiSettings();
            IsReady = true;
            GoogleMap.MyLocationChange += OnLocationChanged;
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