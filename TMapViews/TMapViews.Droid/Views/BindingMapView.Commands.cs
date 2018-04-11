using System.Windows.Input;

namespace TMapViews.Droid.Views
{
    public partial class BindingMapView
    {
        public ICommand CameraMoved { get; set; }
        public ICommand InfoWindowClick { get; set; }
        public ICommand InfoWindowClose { get; set; }
        public ICommand InfoWindowLongClick { get; set; }
        public ICommand LocationChanged { get; set; }
        public ICommand MapClick { get; set; }
        public ICommand MapLongClick { get; set; }
        public ICommand MarkerClick { get; set; }
        public ICommand MarkerDrag { get; set; }
        public ICommand MarkerDragEnd { get; set; }
        public ICommand MarkerDragStart { get; set; }
        public ICommand MyLocationButtonClick { get; set; }
        public ICommand MyLocationClick { get; set; }
        public ICommand OverlayClicked { get; set; }
    }
}