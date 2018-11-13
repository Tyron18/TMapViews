using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using MapKit;
using UIKit;

namespace TMapViews.iOS.Views
{
    public class BindingMKAnnotationView : MKAnnotationView
    {
        public BindingMKAnnotationView()
        {
        }

        public BindingMKAnnotationView(NSCoder coder) : base(coder)
        {
        }

        public BindingMKAnnotationView(CGRect frame) : base(frame)
        {
        }

        public BindingMKAnnotationView(IMKAnnotation annotation, string reuseIdentifier) : base(annotation, reuseIdentifier)
        {
        }

        protected BindingMKAnnotationView(NSObjectFlag t) : base(t)
        {
        }

        protected internal BindingMKAnnotationView(IntPtr handle) : base(handle)
        {
        }

        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            var view = base.HitTest(point, uievent);
            if (view != null)
                Superview.BringSubviewToFront(this);
            return view;
        }

        public override bool PointInside(CGPoint point, UIEvent uievent)
        {
            bool result = Bounds.Contains(point);
            var i = 0;
            while ( !result && i < Subviews.Count())
            {
                var frame = Subviews[i++].Frame;
                result = frame.Contains(point);
            }
            return result;
        }
    }
}