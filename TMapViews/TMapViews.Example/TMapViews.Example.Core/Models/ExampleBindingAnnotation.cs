using MvvmCross.ViewModels;
using TMapViews.Models;

namespace TMapViews.Example.Core.Models
{
    public class ExampleBindingAnnotation : MvxViewModel, IBindingMapAnnotation
    {
        private I2DLocation location;
        private int id;
        private bool _selected;

        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public I2DLocation Location { get => location; set => location = value; }

        public int Id
        {
            get => this.id;
            set => SetProperty(ref this.id, value);
        }

        public (float x, float y) RandomTuple
        {
            get => Id == 1 ? (.5f, .5f) : (0, 0);
        }
    }
}