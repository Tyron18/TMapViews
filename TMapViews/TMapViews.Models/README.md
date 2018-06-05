# TMapViews.Models

The following models are intended to create common return types from all platforms, allowing for easier use of map views with a shared codebase.

## Interfaces

|Interface            |Description                                                   |
|---------------------|--------------------------------------------------------------|
|I2DLocation          |A basic coordinate with latitude and longitude.               |
|I3DLocation          |Extends I2DLocation and adds altitude, accuracy and speed.    |
|IBindingMapAnnotation|Describes an annotation used as a data source for map markers.|
|IBindinMapOverlay    |Extends IBindingMapOverlay to describe map overlays.          |

## Models

|Model            |Description                                                       |
|-----------------|------------------------------------------------------------------|
|Binding2DLocation|Default implimentation of I2DLocation returned by most tap events.|
|Binding3DLocation|Default implimentation of I3Dlocation returned by most gps events.|
