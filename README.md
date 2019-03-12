<p align="left"><img src="logo/horizontal.png" alt="TMapViews" height="120px"></p>

# TMapViews

[![license](https://img.shields.io/github/license/Tyron18/TMapViews.svg)](https://github.com/Tyron18/TMapViews/blob/master/LICENSE) 
[![NuGet](https://img.shields.io/nuget/v/TMapViews.svg)](https://www.nuget.org/packages/TMapViews/)

TMapViews aims to create a more convenients implimentation of maps on IOS and Android in Xamarin. *Boilerplate* code has been rolled into a more userfirendly set-up structure, allowing for easier set-up and life-cycle managment of maps.

Events such as location changed and map clicked have been rolled into commands that return common object typs, allowing for the handling of these events to be done by the shared code by means of single assignments.

Annotations and overlays are managed by the `BindingMapViews` using a DataSource object that is a list of `IBindingAnnotations` that can be assigned to the map. `IBindingMapOverlay` objects are included in the same list.

## Instalation
Get the latest [Nuget Package](https://www.nuget.org/packages/TMapViews/) and install it on both your core and platform solutions.

> Install-Package TMapViews

## Licensing

TMapViews is licensed under the [MIT License](https://github.com/Tyron18/TMapViews/blob/master/LICENSE).

 - TMapViews is based [Xamarin-Android](https://github.com/xamarin/xamarin-android) and [Xamarin-macios](https://github.com/xamarin/xamarin-macios) under the [MIT License](https://github.com/Tyron18/TMapViews/blob/master/LICENSE).
 - MVVMCross is used for the `TMapViews.MvxPlugins.Bindings.Droid` and `TMapViews.MvxPlugins.Bindings.iOS` plugins under the [Microsoft Public License](https://github.com/MvvmCross/MvvmCross/blob/develop/LICENSE). 
 
## Acknowledgements
 - Thanks to [Plac3hold3r](https://github.com/Plac3hold3r) for help and guidance on this project.
