using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFormsMvvmAdaptor;

namespace XfmaShellTemplate.ViewModels
{
    public class AboutViewModel : MvvmViewModelBase
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        public ICommand OpenWebCommand { get; }
    }
}