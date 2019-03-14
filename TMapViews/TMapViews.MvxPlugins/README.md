<p align="left"><img src="../../logo/horizontalv2.png" alt="TMapViews" height="120px"></p>

# TMapViews.MvxPlugins

This pluggin is designed to add functionality to TMapViews to make better use of MvvmCross tools and patterns, including optimised source bindings and BindingContext-aware markers.

On iOS you will need to add the following to your `LinkerPleaseInclude.cs`.

```csharp
public void Include(MvxPlugins.Bindings.iOS.Plugin mapMvxPlugin)
{
    mapMvxPlugin = new MvxPlugins.Bindings.iOS.Plugin();
}
```
