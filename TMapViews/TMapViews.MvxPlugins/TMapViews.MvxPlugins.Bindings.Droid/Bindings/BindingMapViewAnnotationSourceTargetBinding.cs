using MvvmCross.Binding;
using MvvmCross.Platforms.Android.Binding.Target;
using MvvmCross.WeakSubscription;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMapViews.Droid.Views;
using TMapViews.Models;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public class BindingMapViewAnnotationSourceTargetBinding : MvxAndroidTargetBinding<BindingMapView, IEnumerable<IBindingMapAnnotation>>
    {
        private IDisposable _collectionSubscription;
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        public BindingMapViewAnnotationSourceTargetBinding(BindingMapView target) : base(target)
        {
        }

        protected override void SetValueImpl(BindingMapView target, IEnumerable<IBindingMapAnnotation> value)
        {
            if (!ReferenceEquals(value, Target.AnnotationSource))
            {
                _collectionSubscription?.Dispose();
                _collectionSubscription = null;

                Target.AnnotationSource = value;

                if (Target.AnnotationSource is INotifyCollectionChanged cObservable && cObservable != null)
                    _collectionSubscription = cObservable.WeakSubscribe(OnItemSourceCollectionChanged);
            }
        }

        private void OnItemSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (IBindingMapAnnotation item in e.NewItems)
                    {
                        Target.AddAnnotation(item);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (IBindingMapAnnotation item in e.OldItems)
                    {
                        Target.RemoveAnnotation(item);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Target.UpdateAnnotations();
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (IBindingMapAnnotation item in e.OldItems)
                    {
                        Target.RemoveAnnotation(item);
                    }
                    foreach (IBindingMapAnnotation item in e.NewItems)
                    {
                        Target.AddAnnotation(item);
                    }
                    break;

                default:
                    Target.UpdateAnnotations();
                    break;
            }
        }
    }
}