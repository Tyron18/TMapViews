using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using Android.App;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Core;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Binding.Target;
using MvvmCross.ViewModels;

namespace TMapViews.Example.Droid.Linker
{
    // LinkerPleaseInclude is never actually executed, but when Xamarin linking is enabled it ensures types and properties are preserved in the deployed app
    // Included are some examples of types and properties you may need preserved in a linked version of your app
    public class LinkerPleaseInclude
    {
        public void Include(Button button)
        {
            button.Click += (s, e) => button.Text = $"{button.Text}";
        }

        public void Include(CheckBox checkBox)
        {
            checkBox.CheckedChange += (sender, args) => checkBox.Checked = !checkBox.Checked;
        }

        public void Include(TextView text)
        {
            text.AfterTextChanged += (sender, args) => text.Text = $"{text.Text}";
            text.TextChanged += (sender, args) => text.Text = $"{text.Text}";
            text.Hint = $"{text.Hint}";
        }

        public void Include(CompoundButton cb)
        {
            cb.CheckedChange += (sender, args) => cb.Checked = !cb.Checked;
        }

        public void Include(SeekBar sb)
        {
            sb.ProgressChanged += (sender, args) => sb.Progress = sb.Progress + 1;
        }

        public void Include(INotifyCollectionChanged changed)
        {
            changed.CollectionChanged += (s, e) => { var test = $"{e.Action}{e.NewItems}{e.NewStartingIndex}{e.OldItems}{e.OldStartingIndex}"; };
        }

        public void Include(ICommand command)
        {
            command.CanExecuteChanged += (s, e) => { if (command.CanExecute(null)) command.Execute(null); };
        }

        public void Include(MvxPropertyInjector injector)
        {
            injector = new MvxPropertyInjector();
        }

        public void Include(INotifyPropertyChanged changed)
        {
            changed.PropertyChanged += (sender, e) =>
            {
                var test = e.PropertyName;
            };
        }

        public void Include(Activity act)
            => act.Title = $"{act.Title}";

        public void Include(MvxTaskBasedBindingContext context)
        {
            context.Dispose();
            var context2 = new MvxTaskBasedBindingContext();
            context2.Dispose();
        }

        public void Include(MvxViewModelViewTypeFinder viewModelViewTypeFinder)
            => _ = new MvxViewModelViewTypeFinder(null, null);

        public void Include(MvxNavigationService service, IMvxViewModelLoader loader)
        {
            _ = new MvxNavigationService(null, loader);
            service.Navigate(typeof(MvxViewModel));
            service.Navigate("");
            _ = new MvxAppStart<MvxNullViewModel>(null, null);
        }

        public void Include(MvxSettings settings)
            => _ = new MvxSettings();

        public void Include(MvxStringToTypeParser parser)
            => _ = new MvxStringToTypeParser();

        public void Include(MvxViewModelLoader loader)
            => _ = new MvxViewModelLoader(null);

        public void Include(MvxViewModelViewLookupBuilder builder)
            => _ = new MvxViewModelViewLookupBuilder();

        public void Include(MvxCommandCollectionBuilder builder)
            => _ = new MvxCommandCollectionBuilder();

        public void Include(MvxStringDictionaryNavigationSerializer serializer)
            => _ = new MvxStringDictionaryNavigationSerializer();

        public void Include(MvxChildViewModelCache cache)
            => _ = new MvxChildViewModelCache();
    }
}
