using Xamarin.Forms;
using XfmaShellTemplate.Services;
using XamarinFormsMvvmAdaptor;
using XfmaShellTemplate.Models;
using XfmaShellTemplate.Views;

namespace XfmaShellTemplate
{
    public partial class App : Application
    {
        //todo
        //SmartResolve registers Instance using default rules (if successful)
        public App()
        {
            InitializeComponent();

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

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
