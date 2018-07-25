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

        public Marker Marker { get; internal set; }

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

        public float IconScale
        {
            get => _iconScale;
            set
            {
                _iconScale = value;
                UpdateIcon();
            }
        }

        private void UpdateIcon()
        {
            if (Icon != null)
            {
                var bitmap = (Icon as BitmapDrawable).Bitmap;
                var scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, (int)Math.Round(bitmap.Width * IconScale), (int)Math.Round(bitmap.Height * IconScale), false);
                Marker.SetIcon(BitmapDescriptorFactory.FromBitmap(scaledBitmap));
            }
        }
    }
}