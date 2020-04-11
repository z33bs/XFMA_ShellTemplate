using Xamarin.Forms;
using XfmaShellTemplate.Services;
using XamarinFormsMvvmAdaptor;
using XfmaShellTemplate.Models;

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
