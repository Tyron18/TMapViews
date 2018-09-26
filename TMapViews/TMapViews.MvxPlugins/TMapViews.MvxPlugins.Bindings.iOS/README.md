# TMapViews.MvxPlugins.Bindings.iOS

This package is designed to make TMapViews easily compatible with MvvmCross.
`TMapViews.MvxPlugins.Bindings.iOS` adds:
* `MvxBindingMKAnnotationView`
* `MvxBindingMKCallout`
* `MvxBindingMKMapViewDelegate`

## MvxBindingMKAnnotationView
The `MvxBindingMKAnnotationView' is used to create MvvmCross-bindable markers against a `IBindingMapAnnotation` viewmodel.
To use a `MvxBindingMKAnnotationView`, you simply extend the object, and in the constructor, set the view up as needed, creating your bindings in a `DelayBind` action.

### Example
```csharp
public class ExamplePinMvxBindingAnnotationView : MvxBindingMKAnnotationView
{
    public ExamplePinMvxBindingAnnotationView(string reuseIdentifier) : base(reuseIdentifier)
    {
        Draggable = true;
        this.DelayBind(() =>
            {
                var bindingSet = this.CreateBindingSet<ExamplePinMvxBindingAnnotationView, ExampleBindingAnnotation>();
                bindingSet.Bind(this).For(v => v.Image).To(vm => vm.Id).WithDictionaryConversion(new Dictionary<int, UIImage>
                {
                    {1, UIImage.FromBundle("Images/marker_a")},
                    {2, UIImage.FromBundle("Images/marker_b")},
                    {3, UIImage.FromBundle("Images/marker_c")},
                    {4, UIImage.FromBundle("Images/marker_d")},
                    {5, UIImage.FromBundle("Images/marker_e")}
                });
                bindingSet.Apply();
            });
    }
}
```

## MvxBindingMKCallout
This wraps a BindingMkCallout to add a BindingContext for MvvmCross binding.

### Example
```csharp
public class ExampleMvxBindingCallout : MvxBindingCalloutView
{
    UILabel text;
    public ExampleMvxBindingCallout() : base()
    {
        TranslatesAutoresizingMaskIntoConstraints = false;
        Layer.CornerRadius = 5f;
        BackgroundColor = UIColor.Black;
        
        text = new UILabel
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
            TextColor = UIColor.White
        };
        Add(text);
        this.AddConstraints(text.FullSizeOf(this, 5));
        
        this.DelayBind(Bind);
    }
    
    public override nfloat XOffset => 0;
    
    public override nfloat YOffset => -(Frame.Height + 10f);
    
    private void Bind()
    {
        var bindingSet = this.CreateBindingSet<ExampleMvxBindingCallout, ExampleBindingAnnotation>();
        bindingSet.Bind(text).To(vm => vm.Id).WithDictionaryConversion(new Dictionary<int, string>
        {
            {1,"One" },
            {2,"Two" },
            {3,"Three" },
            {4,"Four" },
            {5,"Five" }
        });
        bindingSet.Apply();
    }
}
```

## MvxBindingMKMapViewDelegate
The `MvxBindingMKMapViewDelegate` is used to set your annotation and callout views.

```csharp
public class ExampleBindingMapDelegate : MvxBindingMkMapViewDelegate
{
    public override MvxBindingMKAnnotationView GetViewForBindingAnnotation(MKMapView mapView)
    {
        var result = mapView.DequeueReusableAnnotation("Example") as ExamplePinMvxBindingAnnotationView;
        if (result == null)
            result = new ExamplePinMvxBindingAnnotationView("Example");
        return result;
    }
    
    public override MvxBindingCalloutView GetViewForCallout(MKMapView mapView)
    {
        return new ExampleMvxBindingCallout();
    }
}
```

##Custom Bindings
The following custom binding have also been added:

|Binding |For Object |Description|
|--------|-----------|-----------|
|BindScale()|MKAnnotationView|Allows the scale of the annotation to be bound to an nfloat decimal value|
