
using Android.Content;
using Android.Gms.Maps;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using System;
using System.Collections.Generic;
using TMapViews.Example.Core.Models;
using TMapViews.Models;
using TMapViews.MvxPlugins.Bindings.Droid;

namespace TMapViews.Example.Droid.Views.Fragments
{
    public partial class MultipleMapLocationsFragment
    {
        protected class ExampleInfoWindowAdapter : MvxBindingInfoWindowAdapter
        {
            public ExampleInfoWindowAdapter(IMvxAndroidBindingContext bindingContext, Context context) : base(bindingContext, context)
            {
            }

            public override int? GetViewIdForInfoContent(IBindingMapAnnotation annotation)
                => Resource.Layout.item_info_window;

            public override MvxBindingInfoViewHolder GetInfoWindowViewHolder(View view, IMvxAndroidBindingContext bindingContext)
                => new ExampleInfoWindowViewHolder(view, bindingContext);
        }

        protected class ExampleInfoWindowViewHolder : MvxBindingInfoViewHolder
        {
            private TextView _label;

            public ExampleInfoWindowViewHolder(View view, IMvxAndroidBindingContext bindingContext)
                : base(view, bindingContext)
            {
                _label = view.FindViewById<TextView>(Resource.Id.label_view);

                this.DelayBind(Bind);
            }

            private void Bind()
            {
                MvxFluentBindingDescriptionSet<ExampleInfoWindowViewHolder, ExampleBindingAnnotation> bindingSet = this.CreateBindingSet<ExampleInfoWindowViewHolder, ExampleBindingAnnotation>();
                bindingSet.Bind(_label).To(vm => vm.Id).WithDictionaryConversion(new Dictionary<int, string>
                    {
                        { 1,"One" },
                        { 2,"Two" },
                        { 3,"Three" },
                        { 4,"Four" },
                        { 5,"Five" }
                    });
                bindingSet.Apply();
            }
        }
    }
}