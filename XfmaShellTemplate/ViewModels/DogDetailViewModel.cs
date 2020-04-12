using System;
using Xamarin.Forms;
using XamarinFormsMvvmAdaptor;
using XfmaShellTemplate.Models;
using XfmaShellTemplate.Services;

namespace XfmaShellTemplate.ViewModels
{
    //XFMA - Recieves the QueryString from GoToAsync
    //and sets the property accordingly
    [QueryProperty("DogId", "dogId")]
    public class DogDetailViewModel : MvvmViewModelBase
    {
        readonly IDataStore<Dog> dataStore;

        public DogDetailViewModel(IDataStore<Dog> dataStore)
        {
            this.dataStore = dataStore;
        }

        Dog dog;
        public Dog Dog
        {
            get => dog;
            set => SetProperty(ref dog, value);
        }

        string dogId;
        public string DogId
        {
            get => dogId;
            set => dogId = Uri.UnescapeDataString(value);            
        }

        public override async void OnViewAppearing(object sender, EventArgs e)
        {
            Dog = await dataStore.GetItemAsync(DogId);
        }
    }
}
