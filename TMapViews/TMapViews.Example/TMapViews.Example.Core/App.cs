using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using TMapViews.Example.Core.ViewModels;

namespace TMapViews.Example.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterNavigationServiceAppStart<MultipleMapLocationsViewModel>();
        }
    }
}
