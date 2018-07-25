using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using TMapViews.iOS.Models;

namespace TMapViews.MvxPlugins.Bindings.iOS.Bindings
{
    public class MKAnnotationTitleTargetBinding : MvxTargetBinding<BindingMKAnnotation, string>
    {
        public MKAnnotationTitleTargetBinding(BindingMKAnnotation target) : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValue(string value)
        {
            Target.SetTitle(value);
        }
    }
}