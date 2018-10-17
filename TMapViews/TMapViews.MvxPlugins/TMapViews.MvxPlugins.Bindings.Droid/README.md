# TMapViews.MvxPlugins.Bindings.Droid

This package is designed to make TMapViews easily compatible with MvvmCross. TMapViews.MvxPlugins.Bindings.Droid adds:
* `MvxBindingMapView`
* `MvxBindingMarker`
* `MvxBindingMapViewAdapter`
* `MvxBindingInfoViewHolder`
* `MvxBindingInfoWindowAdapter`

## MvxBindingMapView
The `MvxBindingMapView` adds compatibility for Mvvm patterns to the map. It is required to use any of the above classes, but is implimented the same as a `BindingMapView`.

***Example***
```csharp
private MvxBindingMapView _mapView;
private MvxBindingMapAdapter _mapAdapter;

public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
{
    var view = base.OnCreateView(inflater, container, savedInstanceState);
    _mapView = view.FindViewById<MvxBindingMapView>(Resource.Id.binding_map_view);
    _mapAdapter = new MultpileMapMarkersAdapter(this.Activity);
    var infoWindowAdapter = new ExampleInfoWindowAdapter(BindingContext as IMvxAndroidBindingContext, Context);
    mapView.InfoWindowAdapter = _infoWindowAdapter;
        
    var bindingSet = this.CreateBindingSet<MultipleMapLocationsFragment, MultipleMapLocationsViewModel>();
    bindingSet.Bind(_mapView).For(v => v.AnnotationSource).To(vm => vm.Pins);
    bindingSet.Apply();
    
    return view;
}

public override void OnResume()
{
    base.OnResume();
    _mapView?.Initialize(Activity, _mapAdapter);
}
```

## MvxBindingMarker
The `MvxBindingMarker` establishes the bindings for a marker placed on the map.
**Note:** This does not extend the Android `Marker` object, but wraps it to add functionality.
It contains the following properties :

|Property|Type|Description|
|--------|----|-----------|
|`Icon`|`Drawable`|Sets the drawable to be used to display a marker.|
|`IconScale`|`float`|Scales the marker's size.|
|`Marker`|`Marker`|Returns the `Marker` that will actually be given to the `GoogleMap` object.|

If scaling is not required, it is possible to bind the `Marker` to a `BitmapDescriptor` with the `BindIcon()` binding.

***Example***
```csharp
class ExampleMvxBindingMarker : MvxBindingMarker
{
    public Context Context { get; }

    public ExampleMvxBindingMarker(Context context)
    {
        Context = context;
        this.DelayBind(() =>
        {
            var bindingSet = this.CreateBindingSet<ExampleMvxBindingMarker, ExampleBindingAnnotation>();
            bindingSet.Bind(this).For(v => v.Icon).To(vm => vm.Id).WithDictionaryConversion(new Dictionary<int, Drawable>
                {
                    {1, Context.GetDrawable(Resource.Drawable.marker_a)},
                    {2, Context.GetDrawable(Resource.Drawable.marker_b)},
                    {3, Context.GetDrawable(Resource.Drawable.marker_c)},
                    {4, Context.GetDrawable(Resource.Drawable.marker_d)},          
                    {5, Context.GetDrawable(Resource.Drawable.marker_e)},
                });
            bindingSet.Bind(this).For(v => v.IconScale).To(vm => vm.Selected).WithDictionaryConversion(new Dictionary<bool, float>
                {
                    { true, 1.3f },
                    {false, 1/1.3f }
                });
            bindingSet.Apply();
        });
    }
}
```

## MvxBindingMapViewAdapter
The `MvxBindingMapViewAdapter` manages `MvxBindingMarker` to be used for markers in you data collection. 

***Example***
```csharp
public class MultpileMapMarkersAdapter : MvxBindingMapViewAdapter
{
    public override MvxBindingMarker GetMvxBindingMarker()
    {
        return new ExampleMvxBindingMarker(Context);
    }
    
    public override IJavaObject AddBindingMapOverlay(GoogleMap googleMap, IBindingMapOverlay overlay)
        => null;
}
```

## MvxBindingInfoViewHolder
The `MvxBindingInfoViewHolder` determines the view to be displayed by callouts when a marker is selected. Use a `DelayBind` to apply bindings to the view holder, where the view model the viewmodel for the selected marker.

***Example***
```csharp
public class ExampleInfoWindowViewHolder : MvxBindingInfoViewHolder
{
    private TextView _label;
    
    public ExampleInfoWindowViewHolder(View view, IMvxAndroidBindingContext bindingContext)
        : base(view, bindingContext)
    {
        _label = view.FindViewById<TextView>(Resource.Id.label_view);
        
        this.DelayBind(Bind);
    }

    private void Bind()
    {
        MvxFluentBindingDescriptionSet<ExampleInfoWindowViewHolder, ExampleBindingAnnotation> bindingSet =
            this.CreateBindingSet<ExampleInfoWindowViewHolder, ExampleBindingAnnotation>();
        bindingSet.Bind(_label).To(vm => vm.Id).WithDictionaryConversion(new Dictionary<int, string>
        {
            { 1,"One" },
            { 2,"Two" },
            { 3,"Three" },
            { 4,"Four" },
            { 5,"Five" }
        });
        bindingSet.Apply();
    }
}
```

## MvxBindingInfoWindowAdapter
The `MvxBindingInfoWindowAdapter` manages the views for callouts displayed. If the InfoWindow adapter is not set, calouts will not be enabled.

`GetViewIdForInfoContent` is used to determine the resource layout id for the callout view.

There are 2 ways of displaying a callout, either override the `GetInfoContentsViewHolder` or the `GetInfoWindowViewHolder` methods.
* `GetInfoContentsViewHolder` will display the callout inside of a default `GoogleMap` callout bubble.
* `GetInfoWindowViewHolder` will display the callout without any default layout.
If both of these methods return a view, `GetInfoWindowViewHolder` will take priority. Both otherwise function the same.

***Example***
```csharp
public class ExampleInfoWindowAdapter : MvxBindingInfoWindowAdapter
{
    public ExampleInfoWindowAdapter(IMvxAndroidBindingContext bindingContext, Context context) : base(bindingContext, context)
    {
    }
    
    public override int? GetViewIdForInfoContent(IBindingMapAnnotation annotation)
        => Resource.Layout.item_info_window;

    public override MvxBindingInfoViewHolder GetInfoWindowViewHolder(View view, IMvxAndroidBindingContext bindingContext)
        => new ExampleInfoWindowViewHolder(view, bindingContext);
}
```

## Custom Bindings

|Binding|For Object|Expected Type|Description|
|--------------|--------|----------------------|--------|
|`BindIcon()`  |`Marker`|`BitmapDescriptor`  |Used to bind the icon for a marker to a `BitmapDescriptor`.|
|`BindAnchor()`|`Marker`|`(float x, float y)`|Binds the anchor of the `Marker` so that it's given location is represented by the given location on the marker.|
