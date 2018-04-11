using Android.Runtime;
using System;
using System.Runtime.Serialization;

namespace TMapViews.Droid.Views
{
    [Serializable]
    internal class OverlayAdapterException : System.Exception
    {
        private IJavaObject overlay;

        public OverlayAdapterException()
        {
        }

        public OverlayAdapterException(IJavaObject overlay)
        {
            this.overlay = overlay;
        }

        public OverlayAdapterException(string message) : base(message)
        {
        }

        public OverlayAdapterException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected OverlayAdapterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}