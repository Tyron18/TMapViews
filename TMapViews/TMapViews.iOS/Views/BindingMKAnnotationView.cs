using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using MapKit;
using ObjCRuntime;
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
            while (!result && i < Subviews.Count())
            {
                var frame = Subviews[i++].Frame;
                result = frame.Contains(point);
            }
            return result;
        }

        [Export("layerClass")]
        public static Class LayerClass() => new Class(typeof(ZIndexLayer));

        public virtual nfloat ZIndex
        {
            get => (Layer is ZIndexLayer zIndexLayer) ? zIndexLayer.ZIndex : 0;
            set
            {
                if (Layer is ZIndexLayer zIndexLayer)
                {
                    zIndexLayer.ZIndex = value;
                }
            }
        }
    }

    internal class ZIndexLayer : CALayer
    {
        public ZIndexLayer()
        {

        }

        public ZIndexLayer(CALayer other) : base(other)
        {
        }

        public ZIndexLayer(NSCoder coder) : base(coder)
        {
        }

        protected ZIndexLayer(NSObjectFlag t) : base(t)
        {
        }

        protected internal ZIndexLayer(IntPtr handle) : base(handle)
        {
        }

        public override nfloat ZPosition
        {
            get => base.ZPosition;
            set
            {
                // Throw away system ZPosition 
            }
        }

        public nfloat ZIndex
        {
            get => base.ZPosition;
            set => base.ZPosition = value;
        }
    }
}