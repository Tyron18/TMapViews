# TMapViews.MvxPlugins.Bindings.iOS

Currently this pluggin just adds a binding for user location to source.
On iOS you will need to add the following to your `LinkerPleaseInclude.cs`.

`public void Include(MvxPlugins.Bindings.iOS.Plugin mapMvxPlugin)
{
    mapMvxPlugin = new MvxPlugins.Bindings.iOS.Plugin();
}`
