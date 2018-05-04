using Foundation;
using MvvmCross.Platforms.Ios.Core;
using System;
using TMapViews.Example.Core;
using UIKit;

namespace TMapViews.Example.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        UIWindow _window;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            try
            {
                base.FinishedLaunching(application, launchOptions);
            }catch(Exception e)
            {

            }
            return true;
        }
    }
}
