using CoreGraphics;
using MapKit;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using System;

namespace TMapViews.MvxPlugins.Bindings.iOS.Bindings
{
    public class MKAnnotationViewScaleTargetBinding : MvxTargetBinding<MKAnnotationView, nfloat>
    {
        public static string MKAnnotationViewScaleTargetBindingString = "BindScale";

        public MKAnnotationViewScaleTargetBinding(MKAnnotationView target) : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValue(nfloat value)
        {
            Target.Transform = CGAffineTransform.MakeScale(value, value);
        }
    }
}