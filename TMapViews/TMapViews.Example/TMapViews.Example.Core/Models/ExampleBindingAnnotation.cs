using MvvmCross.ViewModels;
using TMapViews.Models;

namespace TMapViews.Example.Core.Models
{
    public class ExampleBindingAnnotation : MvxViewModel, IBindingMapAnnotation
    {
        private I2DLocation location;
        private int id;

        public I2DLocation Location { get => location; set => location = value; }

        public int Id
        {
            get => this.id;
            set => SetProperty(ref this.id, value);
        }
    }
}