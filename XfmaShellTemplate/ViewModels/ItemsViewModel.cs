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
    //XFMA - All ViewModels must extend this BaseViewModel
    public class ItemsViewModel : MvvmViewModelBase
    {
        readonly INavigationService navigationService;
        readonly IDataStore<Item> dataStore;

        //XFMA - Dependency injection at work
        public ItemsViewModel(INavigationService navigationService, IDataStore<Item> dataStore, IMessagingCenter messagingCenter)
        {
            this.navigationService = navigationService;
            this.dataStore = dataStore;

            Title = "List";
            Items = new ObservableRangeCollection<Item>();

            //XFMA - With different design we can avoid having to use
            //MessagingCenter but this was a nice demonstration of
            //how you can use Interfaces and DI to make code testable
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
                //XFMA ObservableRangeCollection adds the ability to ReplaceRange
                //instead of having to Add items one-by-one
                //See https://github.com/jamesmontemagno/mvvm-helpers#observablerangecollection
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
            => new Command<Item>(async (Item item) =>
            {
                //XFMA - Navigation at work. Note that we can
                //pass any object to the pushed ViewModel
                await navigationService.PushAsync<ItemDetailViewModel>(item);
            });

        public ICommand AddItemCommand
        => new Command(async ()
            => await navigationService.PushAsync<NewItemViewModel>());

        //XFMA - Handles the Page.Appearing event
        //right here in the ViewModel
        public override void OnViewAppearing(object sender, EventArgs e)
        {
            LoadItemsCommand.Execute(null);
        }
    }
}