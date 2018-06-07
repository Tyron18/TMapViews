# TMapViews.iOS

The `BindingMKMapView` and `BindingMKMapViewDelegate` extend the native `MKMapView` and the `MKMapViewDelegate` to reduce the amount of boilerplate code and ato wrap the various events and properties in a way that is helpful to shared-code patterns.

## Getting Started
The constructor for the `BindingMkMapView` takes an optional `BindingMkMapViewDelegate`. The delegate can be set after construction as well. The view will use a standard `BindingMKMapViewDelegate` by default.
`BindingMkMapView map = new BindingMKMapView(mapDelegate);`
