<p align="left"><img src="../../logo/horizontalv2.png" alt="TMapViews" height="120px"></p>

# TMapViews.MvxPlugins

Currently this pluggin just adds a binding for user location to source.
On iOS you will need to add the following to your `LinkerPleaseInclude.cs`.

```csharp
public void Include(MvxPlugins.Bindings.iOS.Plugin mapMvxPlugin)
{
    mapMvxPlugin = new MvxPlugins.Bindings.iOS.Plugin();
}
```
