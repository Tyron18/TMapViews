using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TMapViews.Droid.Adapters;
using TMapViews.Droid.Models;
using TMapViews.Models;
using static Android.Gms.Maps.GoogleMap;

namespace TMapViews.Droid.Views
{
    public class BindingMapView : MapView,
        IOnMapReadyCallback,
        IOnMapClickListener,
        IOnMarkerClickListener
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

        int _mapType = 1;
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

        bool _showUserLocation;
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

        bool _rotateGesturesEnabled;
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

        bool _tiltGesturesEnabled;
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

        bool _scrollGesturesEnabled;
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

        bool _zoomControlsEnabled;
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

        bool _zoomGesturesEnabled;
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

        bool _myLocationButtonEnabled;
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

        bool _annotationsVisible;
        public bool AnnotationsVisible
        {
            get => _annotationsVisible;
            set
            {
                _annotationsVisible = value;
                UpdateAnnotations();
            }
        }

        Binding2DLocation _centerMapLocation;
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

        public ICommand MapClick { get; set; }
        public ICommand MarkerClick { get; set; }
        public ICommand LocationChanged { get; set; }


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
                    else if (annotation is IBindingMapAnnotation)
                    {
                        Adapter.AddBindingMapOverlay(GoogleMap, annotation);
                    }
                }
            }
        }

        public void OnMapClick(LatLng point)
        {
            var loc = Binding2DLocation.FromLatLng(point);
            if (MapClick?.CanExecute(loc) ?? false)
                MapClick.Execute(loc);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            GoogleMap = googleMap;
            UpdateUiSettings();
            IsReady = true;
            GoogleMap.MyLocationChange += OnLocationChanged;
            if (CenterMapLocation != null)
                CenterOn(CenterMapLocation);
            GoogleMap.SetOnMapClickListener(this);
            GoogleMap.SetOnMarkerClickListener(this);
            UpdateAnnotations();
        }

        private void OnLocationChanged(object sender, MyLocationChangeEventArgs e)
        {
            if (e.Location is Binding3DLocation loc && (LocationChanged?.CanExecute(loc) ?? false))
                LocationChanged.Execute(loc);
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

        public bool OnMarkerClick(Marker marker)
        {
            var mAnnotation = marker.Tag;
            if (mAnnotation is IBindingMapAnnotation anno
                && (MarkerClick?.CanExecute(anno) ?? false))
            {
                MarkerClick.Execute(anno);
                return true;
            }
            return false;
        }
    }
}