using Android.Gms.Maps.Model;
using Android.Locations;
using System;
using TMapViews.Droid.Models;
using TMapViews.Models;
using static Android.Gms.Maps.GoogleMap;

namespace TMapViews.Droid.Views
{
    public partial class BindingMapView :
        IOnCameraMoveListener,
        IOnCameraMoveStartedListener,
        IOnCircleClickListener,
        IOnGroundOverlayClickListener,
        IOnInfoWindowClickListener,
        IOnInfoWindowCloseListener,
        IOnInfoWindowLongClickListener,
        IOnMapClickListener,
        IOnMapLongClickListener,
        IOnMarkerClickListener,
        IOnMarkerDragListener,
        IOnMyLocationButtonClickListener,
        IOnMyLocationClickListener,
        IOnPolygonClickListener,
        IOnPolylineClickListener
    {
        private void SetListeners()
        {
            GoogleMap.SetOnCameraMoveListener(this);
            GoogleMap.SetOnCameraMoveStartedListener(this);
            GoogleMap.SetOnCircleClickListener(this);
            GoogleMap.SetOnGroundOverlayClickListener(this);
            GoogleMap.SetOnInfoWindowClickListener(this);
            GoogleMap.SetOnInfoWindowCloseListener(this);
            GoogleMap.SetOnInfoWindowLongClickListener(this);
            GoogleMap.SetOnMapClickListener(this);
            GoogleMap.SetOnMapLongClickListener(this);
            GoogleMap.SetOnMarkerClickListener(this);
            GoogleMap.SetOnMarkerDragListener(this);
            GoogleMap.SetOnMyLocationButtonClickListener(this);
            GoogleMap.SetOnMyLocationClickListener(this);
            GoogleMap.SetOnPolygonClickListener(this);
            GoogleMap.SetOnPolylineClickListener(this);
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

        private void OnLocationChanged(object sender, MyLocationChangeEventArgs e)
        {
            if (e.Location is Binding3DLocation loc && (LocationChanged?.CanExecute(loc) ?? false))
                LocationChanged.Execute(loc);
        }

        public void OnMapClick(LatLng point)
        {
            var loc = Binding2DLocation.FromLatLng(point);
            if (MapClick?.CanExecute(loc) ?? false)
                MapClick.Execute(loc);
        }

        public void OnCameraMove()
        {
            if (CameraMoved?.CanExecute(null) ?? false)
                CameraMoved.Execute(null);
        }

        public void OnCameraMoveStarted(int reason)
        {
            if (CameraMoveStarted?.CanExecute(reason) ?? false)
                CameraMoveStarted.Execute(reason);
        }

        public void OnCircleClick(Circle circle)
        {
            if (circle.Tag is BindingMapOverlay mOverlay
                && (OverlayClicked?.CanExecute(mOverlay) ?? false))
                OverlayClicked.Execute(mOverlay);
        }

        public void OnGroundOverlayClick(GroundOverlay groundOverlay)
        {
            if (groundOverlay.Tag is BindingMapOverlay mOverlay
                && (OverlayClicked?.CanExecute(mOverlay) ?? false))
                OverlayClicked.Execute(mOverlay);
        }

        public void OnInfoWindowClick(Marker marker)
        {
            var mAnnotation = marker.Tag;
            if (mAnnotation is IBindingMapAnnotation anno
                && (InfoWindowClick?.CanExecute(anno) ?? false))
            {
                InfoWindowClick.Execute(anno);
            }
        }

        public void OnInfoWindowClose(Marker marker)
        {
            var mAnnotation = marker.Tag;
            if (mAnnotation is IBindingMapAnnotation anno
                && (InfoWindowClose?.CanExecute(anno) ?? false))
            {
                InfoWindowClose.Execute(anno);
            }
        }

        public void OnInfoWindowLongClick(Marker marker)
        {
            var mAnnotation = marker.Tag;
            if (mAnnotation is IBindingMapAnnotation anno
                && (InfoWindowLongClick?.CanExecute(anno) ?? false))
            {
                InfoWindowLongClick.Execute(anno);
            }
        }

        public void OnMapLongClick(LatLng point)
        {
            var loc = Binding2DLocation.FromLatLng(point);
            if (MapLongClick?.CanExecute(loc) ?? false)
                MapLongClick.Execute(loc);
        }

        public void OnMarkerDrag(Marker marker)
        {
            var mAnnotation = marker.Tag;
            if (mAnnotation is IBindingMapAnnotation anno
                && (MarkerDrag?.CanExecute(anno) ?? false))
            {
                MarkerDrag.Execute(anno);
            }
        }

        public void OnMarkerDragEnd(Marker marker)
        {
            var mAnnotation = marker.Tag;
            if (mAnnotation is IBindingMapAnnotation anno
                && (MarkerDragEnd?.CanExecute(anno) ?? false))
            {
                MarkerDragEnd.Execute(anno);
            }
        }

        public void OnMarkerDragStart(Marker marker)
        {
            var mAnnotation = marker.Tag;
            if (mAnnotation is IBindingMapAnnotation anno
                && (MarkerDragStart?.CanExecute(anno) ?? false))
            {
                MarkerDragStart.Execute(anno);
            }
        }

        public bool OnMyLocationButtonClick()
        {
            if (MyLocationButtonClick?.CanExecute(null) ?? false)
            {
                MyLocationButtonClick.Execute(null);
                return true;
            }
            return false;
        }

        public void OnMyLocationClick(Location location)
        {
            var loc = location as Binding3DLocation;
            if (MyLocationClick?.CanExecute(loc) ?? false)
                MyLocationClick.Execute(loc);
        }

        public void OnPolygonClick(Polygon polygon)
        {
            if (polygon.Tag is BindingMapOverlay mOverlay
                && (OverlayClicked?.CanExecute(mOverlay) ?? false))
                OverlayClicked.Execute(mOverlay);
        }

        public void OnPolylineClick(Polyline polyline)
        {
            if (polyline.Tag is BindingMapOverlay mOverlay
                && (OverlayClicked?.CanExecute(mOverlay) ?? false))
                OverlayClicked.Execute(mOverlay);
        }
    }
}