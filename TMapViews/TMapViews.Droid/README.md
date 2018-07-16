# TMapView.Droid
The `BindingMapView` extends the android native `MapView`, initializing a Google map view, with the aim to reduce the amount of view-side boilerplate code and to wrap events and properties to make them more shared-code friendly.

## Getting Started
The `BindingMapView` requires that you register for a Google Maps Api Key. Instructions on how to do this can be found [here](https://developers.google.com/maps/documentation/android-sdk/signup).

### Adding the map to your layout.
The map can be added to your view using the following name in xml : `TMapViews.Droid.Views.BindingMapView`.
```xml
<TMapViews.Droid.Views.BindingMapView
android:id="@+id/map_view"
android:layout_height="match_parent"
android:layout_width="match_parent" />
```

### Setting up the map view on your view.
The `BindingMapView` needs lifecycle events to be triggered by your view like below:
```csharp
public override void OnResume()
{
	base.OnResume();
	_mapView?.Initialize(Activity);
}

public override void OnPause()
{
	_mapView?.OnPause();
	base.OnPause();
}

public override void OnSaveInstanceState(Bundle outState)
{
	base.OnSaveInstanceState(outState);
	_mapView?.OnSaveInstanceState(outState);
}

public override void OnDestroy()
{
	_mapView?.OnDestroy();
	base.OnDestroy();
}

public override void OnLowMemory()
{
	base.OnLowMemory();
	_mapView?.OnLowMemory();
}
```
*Note*: It is recomended to call the maps `Initialize(Activity)` method on resume.

### The Map Adapter
If you want to use annotations and overlays on your map, you will need a map adapter. The map adapter extends `IBindingMapAdapter`, and determines the properies of the annotations added from your data source.
The adapter is assigned to your map as follows:
```csharp
_mapView.Adapter = new MyMapAdapter(Context);
```

The map adapter has 2 methods, `public IJavaObject AddBindingMapOverlay(GoogleMap googleMap, IBindingMapOverlay overlay)` and `public MarkerOptions GetMarkerOptionsForPin(IBindingMapAnnotation pin)`.

`AddBindingMapOverlay` determines the shape and location of an `IBindingMapOverlay` to be added to the map. It *must* return one of the following or will cause an OverlayAdapterException:
```csharp
Android.Gms.Maps.Model.CircleOption
Android.Gms.Maps.Model.PolygonOptions
Android.Gms.Maps.Model.PolyLineOptions
Android.Gms.Maps.Model.GroundOverlayOptions
```
Example:
```csharp
public IJavaObject AddBindingMapOverlay(GoogleMap googleMap, IBindingMapOverlay overlay)
{
	CircleOptions circleOptions = null;

	if (overlay is ExampleBindingOverlay mOverlay)
	{
		circleOptions = new CircleOptions()
			.InvokeCenter(mOverlay.Location.ToLatLng())
			.InvokeRadius(mOverlay.Radius)
			.InvokeStrokeWidth(0)
			.Clickable(true)
			.InvokeFillColor(Context.GetColor(Android.Resource.Color.HoloBlueLight));
	}

	return circleOptions;
}
```

`GetMarkerOptionsForPin` determines the properties of a marker annotation from your datasource.

Example:
```csharp
public MarkerOptions GetMarkerOptionsForPin(IBindingMapAnnotation pin)
{
	MarkerOptions markerOptions = null;

	if (pin is ExampleBindingAnnotation mPin)
	{
		markerOptions = new MarkerOptions();
		markerOptions.SetPosition(new LatLng(pin.Location.Latitude, pin.Location.Longitude))
			.SetTitle(mPin.Id.ToString())
			.Draggable(true)
		    .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue));
	}

	return markerOptions;
}
```

## Properties

|Property         |Type              |Description                                      |
|-----------------|------------------|-------------------------------------------------|
|`GoogleMap`        |`GoogleMap`        |The core google map that runs the BindingMapView.|
|`Adapter`          |`IBindingMapAdapter`|The adapter used to determine annotation views.  |
|`MapType`          |`int`               |The google map [type](https://developers.google.com/android/reference/com/google/android/gms/maps/GoogleMap.html#MAP_TYPE_HYBRID).|
|`MyLocationEnabled`|`bool`              |Set true to have the map track user location. Requires Permissions|
|`MyLocationButtonEnabled`|`bool`|set true to show the native center-on-user-location button.|
|`RotateGesturesEnabled`|`bool`|Set true to allow the user to rotate the map with gesture controls.|
|`TiltGesturesEnabled`|`bool`|Set true to allow the user to tilt the map viewport with gesture controls.|
|`ScrollGesturesEnabled`|`bool`|Set true to allow the user to scroll the map viewport with gesture controls.|
|`Zoom`|`float`|Sets the camera zoom for the auto-centering on `CenterMapLocation`. ***Note*** `BindingMapView` uses the same zoom values as an iOS `MKMapView`, not standard `GoogleMap` zoom values. In future versions I will add support to toggle between the 2 systems.|
|`ZoomGesturesEnabled`|`bool`|Set true to allow the user to zoom the map viewport with gesture controls.|
|`ZoomControlsEnabled`|`bool`|Set true to allow the user to zoom the map viewport with native button controls.|
|`AnnotationsVisible`|`bool`|Set false to hide annotations.|
|`CenterMapLocation`|`I2DLocation`|Set to center the camera on a set location.|
|`UserLocation`|`I3DLocation`|The users current location. Requires MyLocationEnabled to be true.|
|`IsReady`|`bool`|Used to determine if the GoogleMap has loaded.|
|`AnnotationSource`|`IEnumerable<IBindingMapAnnotation>`|The list of annotations to be displayed on the map.|
|`OverlaySource`|`IEnumerable<IBindingMapOverlay>`|The list of overlays to be displayed on the map.|

## Supported Event Commands

|Event Command        |Return Type          |Description                                                                                                             |
|---------------------|---------------------|------------------------------------------------------------------------------------------------------------------------|
|`CameraMoved`          |`I3DLocation`          |Fires when the viewport of the map changes, returning the center location and "height" of the camera.                   |
|`LocationChanged`      |`I3DLocation`          |Fires when the user's location changes, returning GPS data *Note* Requires 'MyLocationEnabled = true'.                  |
|`MapClick`             |`I2DLocaiton`          |Fires when the user taps on the map, returning the coordinates of the map.                                              |
|`MapLongClick`         |`I2DLocation`          |Fires when the user long taps on the map, returning the coordinates of the map.                                         |
|`MarkerClick`          |`IBindingMapAnnotation`|Fires when the user taps on a marker annotation, returning the annotation object from the annotation data source.       |
|`MarkerDrag`           |`IBindingMapAnnotation`|Fires when the user drags a marker annotation, returning the annotation object from the annotation data source.         |
|`MarkerDragStart`      |`IBindingMapAnnotation`|Fires when the user begins to drag a marker annotation, returning the annotation object from the annotation data source.|
|`MyLocationButtonClick`|`void`             |Fires when the user taps on the native "center on my location" button.                                                  |
|`MyLocationClick`      |`IBinding3dLocation`   |Fires when the user taps on the native user location marker annotation, returning the user's location                   |
|`OverlayClick`         |`IBindingMapAnnotation`|Fires when the user taps on an overlay, returning the annotation object from the data source.                           |

## Public Methods

`int Initialize(Activity context, IBindingMapAdapter adapter = null, Bundle savedInstanceState = null)`
Initializes the map if GooglePlayServices are available. Return result codes from [`Android.Gms.Common.ResultCode`](https://developers.google.com/android/reference/com/google/android/gms/common/ConnectionResult.html#API_UNAVAILABLE). Returns 0 on success.

`void GetMapsAsync(IBindingMapAdapter adapter = null, Bundle savedInstanceState = null)`
Initiates the `GoogleMap` creation. This is called by `Initialize` which is the preffered method of refreshing your map.
