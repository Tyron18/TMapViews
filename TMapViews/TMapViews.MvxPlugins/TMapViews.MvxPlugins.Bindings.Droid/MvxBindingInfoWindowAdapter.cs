using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using MvvmCross.Commands;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using System;
using TMapViews.Droid.Models;
using TMapViews.Models;
using static Android.Gms.Maps.GoogleMap;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    public class MvxBindingInfoWindowAdapter : Java.Lang.Object,
        IInfoWindowAdapter
    {
        private IMvxCommand<IBindingMapAnnotation> _infoWindowClick;
        private IMvxCommand<IBindingMapAnnotation> _infoWindowClose;
        private IMvxCommand<IBindingMapAnnotation> _infoWindowLongClick;
        private bool _infoWindowClickOverloaded;
        private bool _infoWindowCloseOverloaded;
        private bool _infoWindowLongClickOverloaded;
        private readonly IMvxAndroidBindingContext _bindingContext;
        internal GoogleMap MapView { get; set; }
        private readonly Context _context;

        public MvxBindingInfoWindowAdapter(IMvxAndroidBindingContext bindingContext, Context context)
        {
            _bindingContext = bindingContext;
            _context = context;
        }

        public View GetInfoContents(Marker marker)
        {
            var anno = (marker.Tag as AnnotationTag).Annotation;
            int? id = GetViewIdForInfoContent(anno);
            if (id != null)
            {
                MvxAndroidBindingContext bindingContext = new MvxAndroidBindingContext(_context, _bindingContext.LayoutInflaterHolder);
                View view = bindingContext.BindingInflate(id.Value, null);
                MvxBindingInfoViewHolder vh = GetInfoContentsViewHolder(view, bindingContext);

            }
            return null;
        }

        public virtual MvxBindingInfoViewHolder GetInfoContentsViewHolder(View view, IMvxAndroidBindingContext bindingContext)
            => null;

        public virtual MvxBindingInfoViewHolder GetInfoWindowViewHolder(View view, IMvxAndroidBindingContext bindingContext)
            => null;

        public virtual int? GetViewIdForInfoContent(IBindingMapAnnotation annotation)
            => null;

        public View GetInfoWindow(Marker marker)
        {
            var anno = (marker.Tag as AnnotationTag).Annotation;
            int? id = GetViewIdForInfoContent(anno);
            if(id != null)
            {
                MvxAndroidBindingContext bindingContext = new MvxAndroidBindingContext(_context, _bindingContext.LayoutInflaterHolder);
                View view = bindingContext.BindingInflate(id.Value, null);
                MvxBindingInfoViewHolder vh = GetInfoWindowViewHolder(view, bindingContext);
                vh.DataContext = anno;
                return view;
            }
            return null;
        }
        
        public IMvxCommand<IBindingMapAnnotation> InfoWindowClick
        {
            get => _infoWindowClick;
            set
            {
                _infoWindowClick = value;
                EnsureInfoWindowClickOverloaded();
            }
        }

        private void EnsureInfoWindowClickOverloaded()
        {
            if (!_infoWindowClickOverloaded)
            {
                _infoWindowClickOverloaded = true;
                MapView.InfoWindowClick += OnInfoWindowClick;
            }
        }

        private void OnInfoWindowClick(object sender, InfoWindowClickEventArgs e)
            => ExecuteCommand(InfoWindowClick, e.Marker);

        private void ExecuteCommand(IMvxCommand<IBindingMapAnnotation> command, Marker marker)
        {
            if (command == null)
                return;

            var anno = (marker.Tag as AnnotationTag).Annotation;
            if (anno == null)
                return;

            if (command.CanExecute(anno))
                command.Execute(anno);
        }

        public IMvxCommand<IBindingMapAnnotation> InfoWindowClose
        {
            get => _infoWindowClose;
            set
            {
                _infoWindowClose = value;
                EnsureInfoWindowCloseOverloaded();
            }
        }

        private void EnsureInfoWindowCloseOverloaded()
        {
            if(!_infoWindowCloseOverloaded)
            {
                _infoWindowCloseOverloaded = true;
                MapView.InfoWindowClose += OnInfoWindowClose;
            }
        }

        private void OnInfoWindowClose(object sender, InfoWindowCloseEventArgs e)
            => ExecuteCommand(InfoWindowClose, e.Marker);

        public IMvxCommand<IBindingMapAnnotation> InfoWindowLongClick
        {
            get => _infoWindowLongClick;
            set
            {
                _infoWindowLongClick = value;
                EnsureInfoWindowLongClickOverloaded();
            }
        }

        private void EnsureInfoWindowLongClickOverloaded()
        {
            if(!_infoWindowLongClickOverloaded)
            {
                _infoWindowLongClickOverloaded = true;
                MapView.InfoWindowLongClick += OnInfoWindowLongClick;
            }
        }

        private void OnInfoWindowLongClick(object sender, InfoWindowLongClickEventArgs e)
            => ExecuteCommand(InfoWindowLongClick, e.Marker);
    }
}