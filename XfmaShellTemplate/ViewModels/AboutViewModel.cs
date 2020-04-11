using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsMvvmAdaptor;

namespace XfmaShellTemplate.ViewModels
{
    public class AboutViewModel : MvvmViewModelBase
    {
        readonly INavigationService navigationService;

        public AboutViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Title = "About";
        }

        //XFMA - Another example of GoToAsync, using routes
        public ICommand JumpCommand
            => new Command(async () => await navigationService.GoToAsync("///dogs"));
    }
}