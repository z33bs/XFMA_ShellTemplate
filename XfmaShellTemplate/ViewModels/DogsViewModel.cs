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
    public class DogsViewModel : MvvmViewModelBase
    {
        readonly INavigationService navigationService;
        readonly IDataStore<Dog> dataStore;

        public DogsViewModel(INavigationService navigationService, IDataStore<Dog> dataStore)
        {
            this.navigationService = navigationService;
            this.dataStore = dataStore;

            Title = "Dogs";
            Dogs = new ObservableRangeCollection<Dog>();
        }

        public ObservableRangeCollection<Dog> Dogs { get; }

        public ICommand LoadDogsCommand
            => new Command(async () => await ExecuteLoadDogsCommand());
        async Task ExecuteLoadDogsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                var items = await dataStore.GetItemsAsync(true);
                Dogs.ReplaceRange(items);
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

        public ICommand DogSelectedCommand
            => new Command<Dog>(async (Dog item)
                => await navigationService.GoToAsync($"dogDetail?dogId={item.Id}"));

        public override void OnViewAppearing(object sender, EventArgs e)
        {
            LoadDogsCommand.Execute(null);
        }
    }
}