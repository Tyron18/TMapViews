using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using MvvmCross;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.WeakSubscription;
using TMapViews.iOS;
using TMapViews.iOS.Models;
using TMapViews.Models;

namespace TMapViews.MvxPlugins.Bindings.iOS
{
    [Preserve(AllMembers = true)]
    public class BindingMKMapViewAnnotationTargetBinding : MvxTargetBinding<BindingMKMapView, IEnumerable<IBindingMapAnnotation>>
    {
        private IDisposable _collectionSubscription;

        public BindingMKMapViewAnnotationTargetBinding(BindingMKMapView target) : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValue(IEnumerable<IBindingMapAnnotation> value)
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
                        Target.AddAnnotation(new BindingMKAnnotation(item));
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (IBindingMapAnnotation item in e.OldItems)
                    {
                        var oldItem = Target.Annotations.SingleOrDefault(x => (x is BindingMKAnnotation anno) && ReferenceEquals(anno.Annotation, item));
                        Target.RemoveAnnotation(oldItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Target.UpdatePins();
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (IBindingMapAnnotation item in e.OldItems)
                    {
                        var oldItem = Target.Annotations.SingleOrDefault(x => (x is BindingMKAnnotation anno) && ReferenceEquals(anno.Annotation, item));
                        Target.RemoveAnnotation(oldItem);
                    }
                    foreach (IBindingMapAnnotation item in e.NewItems)
                    {
                        Target.AddAnnotation(new BindingMKAnnotation(item));
                    }
                    break;

                default:
                    Target.UpdatePins();
                    break;
            }
        }
    }
}