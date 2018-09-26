# TMapViews.iOS

The `BindingMKMapView` and `BindingMKMapViewDelegate` extend the native `MKMapView` and the `MKMapViewDelegate` to reduce the amount of boilerplate code and ato wrap the various events and properties in a way that is helpful to shared-code patterns.

## Getting Started
The constructor for the `BindingMkMapView` takes an optional `BindingMkMapViewDelegate`. The delegate can be set after construction as well. The view will use a standard `BindingMKMapViewDelegate` by default.
`BindingMkMapView map = new BindingMKMapView(mapDelegate);`

## The Delegate
If you want to use annotations and overlays on your map, you will need a custom map delegate. The map map delegate extends `BindingMkMapViewDelegate`, and determines the properies of the annotations added from your data source. The adapter is assigned to your map as follows:

`_mapView.Delegate = new MyMapDelegate();`

The map delegate contatains 2 overridable methods. `MKAnnotationView GetViewForBindingAnnotation(MKMapView mapView, IBindingMapAnnotation bindingMapAnnotation)` and `IBindingMKMapOverlay GetViewForBindingOverlay(MKMapView mapView, IBindingMapOverlay bindingMapOverlay)`. Used to generate markers and overlays respectively.

`GetViewForBindingAnnotation` determines the look of a `IBindingMapAnnotation` to be added to the map. It returns a native `MKAnnotation`. This method effectively works the same as the native `GetViewForAnnotation` but recieves am `IBindingMapAnnotation` as a paramter instead of a `IMKAnnotation`.
*Note* The annotation **must** have a Title and Subtitle in order for IOS to render the marker.

Example:

```csharp
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
            view.Annotation = annotation;

        view.Draggable = true;
        view.Image = UIImage.FromBundle("Images/marker_a");

        return view;
    }
    return null;        //Lets the map default behavior take over if the annotation isnt a BindingMKAnnotation.
}
```

`GetViewForBindingOverlay` creates the overlay to be placed on the map for an `IBindingMapOverlay`, returning an `IBindingMKMapOverlay`.
The `IBindingMKMapOverlay` is an extended `MKMapOverlay` that includes an `IBindingMapAnnotation Annotation` and a `MKOverlayRenderer Renderer`.
The `Annotation` property will be set by the `BindingMKMapView` and does not need to be set by this method.
The `Renderer` must then be set in the `OverlayRenderer` method, it includes properties such as fill color and stroke width.
    For more info on the render, check out the [Apple](https://developer.apple.com/documentation/mapkit/mkoverlayrenderer) and [Xamarin](https://developer.xamarin.com/api/type/MonoTouch.MapKit.MKOverlayRenderer/) documentation.

There are 4 included `IBindingMKMapOverlay`:

|Overlay               |Basic Renderer         |Creation Method                                                                   |
|----------------------|-----------------------|----------------------------------------------------------------------------------|
|`BindingMKCircle`     |`MKCircleRenderer`     |`BindingMKCircle.Circle(/*CLLocationCoordinate2D*/centerCoord,/*double*/radius);`|
|`BindingMKPolygon`    |`MKPolygonRenderer`    |`BindingMKPolygon.FromCoordinates(/*CLLocationCoordinate2D[]*/coords);` `BindingMKPolygon.FromCoordinates(/*CLLocationCoordinate2D[]*/coords,/*MKPolygon[]*/interiorPolygons);` `BindingMKPolygon.FromPoints(/*MKMapPoints[]*/points);` `BindingMKPolygon.FromPoints(/*MKMapPoints[]*/points,/*MKPolygon[]*/interiorPolygons);`|
|`BindingMKPolyline`   |`MKPolylineRenderer`   |`BindingMKPolyline.FromCoordinates(/*CLLocationCoordinate2D[]*/coords);` `BindingMKPolyline.FromPoints(/*MKMapPoint[]*/points);`|
|`BindingMKTileOverlay`|`MKTileOverlayRenderer`|`new BindingMKTileOverlay(/*string*/URLTemplate);`|

Example:

```csharp
public override IBindingMKMapOverlay GetViewForBindingOverlay(MKMapView mapView, IBindingMapOverlay bindingMapOverlay)
{
    if (bindingMapOverlay is ExampleBindingOverlay eOverlay)
    {
        return BindingMKCircle.Circle(eOverlay.Location.ToCLLocationCoordinate2D(), eOverlay.Radius);
    }
    return base.GetViewForBindingOverlay(mapView, bindingMapOverlay);
}

public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlay)
{
    return new MKCircleRenderer(overlay)
        {
            StrokeColor = UIColor.Blue,
            LineWidth = 1f,
            FillColor = UIColor.Gray
        };
}
```

## Properties
### BindingMKMapView

|Property|Type|Description|
|--------------------|------------------------------------|------------------------------------------------------------------------|
|`AnnotationsVisible`|`bool`                              |Toggles whether or not to show annotations on the map view. (Including overlays)|
|`ZoomLevel`         |`double`                            |Sets the zoom level of the camera when centering, according to IOS standard.|
|`CenterMapLocation` |`I2DLocation`                       |Sets the center point of the camera.|
|`ShowUserLocation`  |`bool`                              |Sets whether or not to track and show user location.|
|`UserLocation`      |`I3DLocation`                       |Returns the user's current location. Requires `ShowUserLocation = true;`|
|`AnnotationSource`  |`IEnumerable<IBindingMapAnnotation>`|Determines the source of the map annoptations.|
|`OverlaySource`     |`IEnumerable<IBindingMapOverlay>`   |Determines the source of the map overlays.|
|`Delegate`          |`BindingMkMapViewDelegate`          |Assigns the maps delegat.|
|`MapType`           |`MKMapType`                         |Determines the [display type](https://developer.apple.com/documentation/mapkit/mkmaptype) of the map.|

## Supported Event Commands
### BindingMKMapView

|Event Command|Return Type|Description|
|--------------|-------------------|-------------------------------------------------------------------------------|
|`MapClick`    |`Binding2DLocation`|Fires when the user taps on the map, returning the coordinates of the tap.     |
|`MapLongClick`|`Binding2DLocation`|Fires when the user long-taps on the map, returning the coordinates of the tap.|

### BindingMKMapViewDelegate

|Event Command|Return Type|Description|
|-------------------|-----------------------|--------------------------------------------------------------------------------|
|`Locationchanged`  |`Binding3DLocation`    |Fires when the user's location changes. Requires `ShowUserLocation = true` on the map view.|
|`MarkerClick`      |`IBindingMapAnnotation`|Fires when the user taps on an annotation. *Note:* Currently, the map view `MapClick` will also fire when the user taps an annotation.|
|`OverlayClicked`   |`IBindingMapAnnotation`|Fires when the user taps on an overlay. *Note:* Currently, the map view `MapClick` will also fire when the user taps an overlay.|
|`MarkerDeselected` |`IBindingMapAnnotation`|Fires when an annotation is deselected.|
|`OverlayDeselected`|`IBindingMapAnnotation`|Fires when an overlay is deselected.|
|`MyLocationClick`  |`I3DLocation`          |Fires when the user taps on the native user location marker.|
|`MarkerDrag`       |`IBindingMapAnnotation`|Fires when a marker is dragged by the user.|
|`MarkerDragStart`  |`IBindingMapAnnotation`|Fires when the user begins dragging a marker.|
|`MarkerDrageEnd`   |`IBindingMapAnnotation`|Fires when the user stops dragging a marker.|
|`CameraMoved`      |`Binding3DLocation`    |Fires when the viewport of the map changes.|
