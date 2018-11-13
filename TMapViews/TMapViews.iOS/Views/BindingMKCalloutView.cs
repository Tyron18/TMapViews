using System;
using Cirrious.FluentLayouts.Touch;
using TMapViews.Models;
using UIKit;

namespace TMapViews.iOS.Views
{
    public class BindingMKCalloutView : UIView
    {
        public BindingMKCalloutView()
        {
        }

        protected nfloat _xOffset;
        protected nfloat _yOffset;

        public virtual nfloat XOffset
        {
            get => _xOffset;
            set
            {
                _xOffset = value;
                if (XPosLayout != null)
                    XPosLayout.Constant = _xOffset;
            }
        }

        public virtual nfloat YOffset
        {
            get => _yOffset;
            set
            {
                _yOffset = value;
                if (YPosLayout != null)
                    YPosLayout.Constant = _yOffset;
            }
        }

        internal FluentLayout XPosLayout { get; set; }
        internal FluentLayout YPosLayout { get; set; }

        internal IBindingMapAnnotation Annotation { get; set; }
    }
}