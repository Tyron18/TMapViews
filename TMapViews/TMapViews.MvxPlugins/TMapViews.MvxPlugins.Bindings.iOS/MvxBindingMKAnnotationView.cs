using Foundation;
using MapKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using TMapViews.iOS.Models;
using TMapViews.iOS.Views;
using TMapViews.Models;

namespace TMapViews.MvxPlugins.Bindings.iOS
{
    [Preserve(AllMembers = true)]
    public abstract class MvxBindingMKAnnotationView : BindingMKAnnotationView, IMvxBindable
    {
        private string _reuseIdentifier;

        public override string ReuseIdentifier { get => _reuseIdentifier; }

        public new BindingMKAnnotation Annotation
        {
            get => base.Annotation as BindingMKAnnotation;
            set
            {
                base.Annotation = value;
                OnAnnotationSet();
            }
        }

        public MvxBindingMKAnnotationView()
        {
            this.CreateBindingContext(string.Empty);
        }

        public MvxBindingMKAnnotationView(string reuseIdentifier)
        {
            _reuseIdentifier = reuseIdentifier;
            this.CreateBindingContext(string.Empty);
        }

        public IMvxBindingContext BindingContext { get; set; }

        public object DataContext
        {
            get => BindingContext.DataContext;
            set
            {
                if (value is IBindingMapAnnotation val)
                {
                    Annotation = new BindingMKAnnotation(val);
                    BindingContext.DataContext = val;
                }
            }
        }

        public virtual void OnAnnotationSet()
        {
        }
    }
}