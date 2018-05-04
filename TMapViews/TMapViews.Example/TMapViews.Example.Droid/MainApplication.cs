using System;
using Android.App;
using Android.Runtime;
using MvvmCross.Platforms.Android.Views;
using TMapViews.Example.Core;

[assembly: MetaData("com.google.android.maps.v2.API_KEY", Value = "AIzaSyAqKkqjIyLNth0ED4eXnzHDz8MyE8Pr-qs")]
namespace TMapViews.Example.Droid
{
    [Application]
    public class MainApplication : MvxAndroidApplication<Setup, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}