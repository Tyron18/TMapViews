using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using System;

namespace TMapViews.MvxPlugins.Bindings.Droid
{
    [Preserve(AllMembers = true)]
    public abstract class MvxBindingMarker : IMvxBindingContextOwner, IMvxDataConsumer
    {
        public MvxBindingMarker() : base()
        {
            this.CreateBindingContext(string.Empty);
        }

        public object DataContext { get => BindingContext.DataContext; set => BindingContext.DataContext = value; }
        public IMvxBindingContext BindingContext { get; set; }

        public Marker Marker
        {
            get => _marker;
            set
            {
                _marker = value;
                _marker.ZIndex = _zIndex;
                if (!(_anchor is null))
                    _marker.SetAnchor(_anchor.X, _anchor.Y);
                if (!(_icon is null))
                    UpdateIcon();
            }
        }

        private Drawable _icon;

        public Drawable Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                UpdateIcon();
            }
        }

        private float _iconScale = 1;
        private PointF _anchor;
        private Marker _marker;
        private float _zIndex;

        public float IconScale
        {
            get => _iconScale;
            set
            {
                _iconScale = value;
                UpdateIcon();
            }
        }

        public PointF Anchor
        {
            get => _anchor;
            set
            {
                _anchor = value;
                if (!(_anchor is null))
                    _marker?.SetAnchor(_anchor.X, _anchor.Y);
            }
        }

        public float ZIndex
        {
            get => _zIndex;
            set
            {
                _zIndex = value;
                if (!(_marker is null))
                    _marker.ZIndex = _zIndex;
            }
        }

        private void UpdateIcon()
        {
            using (BitmapDescriptor icon = GetIcon())
            {
                if (!(icon is null))
                    Marker?.SetIcon(icon);
            }
        }

        public BitmapDescriptor GetIcon()
        {
            if (Icon != null)
            {
                using (Bitmap bitmap = (Icon as BitmapDrawable).Bitmap)
                using (Bitmap scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, (int)Math.Round(bitmap.Width * IconScale), (int)Math.Round(bitmap.Height * IconScale), false))
                {
                    BitmapDescriptor bitmapDescriptor = BitmapDescriptorFactory.FromBitmap(scaledBitmap);
                    return bitmapDescriptor;
                }
            }
            return null;
        }
    }
}