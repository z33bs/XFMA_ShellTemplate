//#define STRICT_MODE //For enterprise applications

using Xamarin.Forms;
using XamarinFormsMvvmAdaptor;
using XfmaShellTemplate.Models;
using XfmaShellTemplate.Services;
using XfmaShellTemplate.ViewModels;
using XfmaShellTemplate.Views;

namespace XfmaShellTemplate
{
    public class App : Application
    {
        public App()
        {
#if !STRICT_MODE
            //XFAM - Register Dependencies with minimal code
            SimpleRegistration();
#else
            //XFMA - A more verbose but more disciplined approach
            //to registering dependencies
            EnterpriseRegistration();
#endif
            //XFMA - Used to register the route for GoToAsync
            //navigation from DogsPage
            Routing.RegisterRoute("dogDetail", typeof(DogDetailPage));

            MainPage = new AppShell();
        }

        private static void SimpleRegistration()
        {
            //XFMA Smart Resolve functionality will create the
            //ViewModels if their depenencies can be resolved
            //Here we are only registering Services
            ViewModelLocator.Ioc
                .Register<MessagingCenter>()
                    .As<IMessagingCenter>()
                    .SingleInstance();

            //Default behaviour is SingleInstance if .As<>()
            //is used in the registration. Applied from hereon...
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
        }

        //XFMA - A more verbose but more disciplined approach
        //to registering dependencies
        public void EnterpriseRegistration()
        {
            ViewModelLocator.Ioc
                .ConfigureResolveMode(isStrictMode: true);

            //We can also customise the naming conventions...
            //example below is same as the defaults
            ViewModelLocator.Configure()
                .SetViewAssemblyQualifiedNamespace<AboutPage>()
                .SetViewModelAssemblyQualifiedNamespace<AboutViewModel>()
                .SetViewSuffix("Page")
                .SetViewModelSuffix("ViewModel");

            //Register Services
            ViewModelLocator.Ioc
                .Register<MessagingCenter>()
                .As<IMessagingCenter>()
                .SingleInstance();
            ViewModelLocator.Ioc
                .Register<NavigationService>()
                .As<INavigationService>()
                .SingleInstance();
            ViewModelLocator.Ioc
                .Register<MockDataStore>()
                .As<IDataStore<Item>>()
                .SingleInstance();
            ViewModelLocator.Ioc
                .Register<MockDogStore>()
                .As<IDataStore<Dog>>()
                .SingleInstance();

            //Register ViewModels
            ViewModelLocator.Ioc
                .Register<ItemsViewModel>()
                .SingleInstance();

            //Default MultiInstance if .As<>() is not set...
            ViewModelLocator.Ioc
                .Register<ItemDetailViewModel>();
            //...but we'll be explicit from here on
            ViewModelLocator.Ioc
                .Register<NewItemViewModel>()
                .MultiInstance();
            ViewModelLocator.Ioc
                .Register<DogsViewModel>()
                .SingleInstance();
            ViewModelLocator.Ioc
                .Register<DogDetailViewModel>()
                .MultiInstance();
            ViewModelLocator.Ioc
                .Register<AboutViewModel>()
                .SingleInstance();
        }
    }
}
