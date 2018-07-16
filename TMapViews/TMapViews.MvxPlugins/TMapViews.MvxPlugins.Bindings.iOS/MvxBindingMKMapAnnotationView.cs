using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using MapKit;
using MvvmCross.Binding.BindingContext;
using TMapViews.iOS.Models;
using UIKit;

namespace TMapViews.MvxPlugins.Bindings.iOS
{
    public class MvxBindingMKMapAnnotationView : MKAnnotationView, IMvxBindingContextOwner
    {
        public MvxBindingMKMapAnnotationView() : this(string.Empty)
        {
        }

        public MvxBindingMKMapAnnotationView(string bindingDescription)
        {
            this.CreateBindingContext(bindingDescription);
        }

        public MvxBindingMKMapAnnotationView(NSCoder coder) : base(coder)
        {
        }

        public MvxBindingMKMapAnnotationView(CGRect frame) : base(frame)
        {
        }

        public MvxBindingMKMapAnnotationView(IMKAnnotation annotation, string reuseIdentifier) : base(annotation, reuseIdentifier)
        {
            if (annotation is IBindingMKMapAnnotation anno)
                DataContext = anno;
            this.CreateBindingContext();
        }

        protected MvxBindingMKMapAnnotationView(NSObjectFlag t) : base(t)
        {
        }

        protected internal MvxBindingMKMapAnnotationView(IntPtr handle) : base(handle)
        {
        }

        public IMvxBindingContext BindingContext { get; set; }

        public IBindingMKMapAnnotation DataContext
        {
            get => BindingContext.DataContext as IBindingMKMapAnnotation;
            set => BindingContext.DataContext = value;
        }
    }
}