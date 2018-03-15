using TMapViews.Models;

namespace TMapViews.Droid.Models
{
    public class BindingMapOverlay : Java.Lang.Object, IBindingMapAnnotation
    {
        public virtual I2DLocation Location { get; set; }
    }
}