using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsMvvmAdaptor;
using XfmaShellTemplate.Models;

namespace XfmaShellTemplate.ViewModels
{
    public class NewItemViewModel : MvvmViewModelBase
    {
        readonly INavigationService navigationService;
        readonly IMessagingCenter messagingCenter;

        public NewItemViewModel(INavigationService navigationService, IMessagingCenter messagingCenter)
        {
            this.navigationService = navigationService;
            this.messagingCenter = messagingCenter;

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };
        }

        public Item Item { get; set; }

        public ICommand SaveCommand
            => new Command(async () =>
            {
                messagingCenter.Send(this, "AddItem", Item);
                await navigationService.PopAsync();
            });

        public ICommand CancelCommand
            => new Command(async () =>
            {
                await navigationService.PopAsync();
            });
    }
}
