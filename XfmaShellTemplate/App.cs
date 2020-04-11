using Xamarin.Forms;
using XamarinFormsMvvmAdaptor;
using XfmaShellTemplate.Models;
using XfmaShellTemplate.Services;
using XfmaShellTemplate.Views;

namespace XfmaShellTemplate
{
    public class App : Application
    {
        public App()
        {
            //Register Dependencies
            ViewModelLocator.Ioc
                .Register<MessagingCenter>()
                    .As<IMessagingCenter>().SingleInstance();
            ViewModelLocator.Ioc
                .Register<NavigationService>()
                    .As<INavigationService>();
            ViewModelLocator.Ioc
                .Register<MockDataStore>()
                .As<IDataStore<Item>>();
            ViewModelLocator.Ioc
                .Register<MockDogStore>()
                .As<IDataStore<Dog>>()
                .SingleInstance();

            Routing.RegisterRoute("dogDetail", typeof(DogDetailPage));

            MainPage = new AppShell();
        }
    }
}
