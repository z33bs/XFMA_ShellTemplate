using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsMvvmAdaptor;
using XfmaShellTemplate.Models;
using XfmaShellTemplate.Services;

namespace XfmaShellTemplate.ViewModels
{
    public class ItemsViewModel : MvvmViewModelBase
    {
        readonly INavigationService navigationService;
        readonly IDataStore<Item> dataStore;

        public ItemsViewModel(INavigationService navigationService, IDataStore<Item> dataStore, IMessagingCenter messagingCenter)
        {
            this.navigationService = navigationService;
            this.dataStore = dataStore;

            Title = "Browse";
            Items = new ObservableRangeCollection<Item>();

            messagingCenter.Subscribe<NewItemViewModel, Item>(this, "AddItem", async (obj, item) =>
            {
                await dataStore.AddItemAsync(item);
            });
        }

        public ObservableRangeCollection<Item> Items { get; }

        public ICommand LoadItemsCommand
            => new Command(async () => await ExecuteLoadItemsCommand());
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                var items = await dataStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ICommand ItemSelectedCommand
            => new Command<Item>(async (Item item)
                => await navigationService.PushAsync<ItemDetailViewModel>(item));

        public ICommand AddItemCommand
            => new Command(async ()
                => await navigationService.PushAsync<NewItemViewModel>());

        public override void OnViewAppearing(object sender, EventArgs e)
        {
            LoadItemsCommand.Execute(null);
        }
    }
}