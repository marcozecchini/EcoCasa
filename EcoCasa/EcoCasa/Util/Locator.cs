using EcoCasa.ViewModel;
using EcoCasa.Views;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace EcoCasa.Util
{
    public class Locator
    {
        /// <summary>
        /// Register all the used ViewModels, Services et. al. witht the IoC Container
        /// </summary>
        static Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            //Configure NavigationService
            var nav = new NavigationService();

            //Add each view here and down
            nav.Configure(Locator.MainPage, typeof(MainPage));
            nav.Configure(Locator.LogInPage, typeof(LogInPage));
            nav.Configure(Locator.RegistrationPage,typeof(RegistrationPage));
            nav.Configure(Locator.LogFbPage, typeof(LogFbPage));
            nav.Configure(Locator.ProfilePage,typeof(ProfilePage));
            nav.Configure(Locator.CreateSmartCasaPage, typeof(CreateSmartCasaPage));
            nav.Configure(Locator.SmartCasaPage, typeof(SmartCasaPage));

            //Singleton di NavigationService
            SimpleIoc.Default.Register(() => nav);

            // ViewModels
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LogInViewModel>();
            SimpleIoc.Default.Register<RegistrationViewModel>();
            SimpleIoc.Default.Register<FacebookViewModel>();
            SimpleIoc.Default.Register<ProfileViewModel>();
            SimpleIoc.Default.Register<CreateSmartCasaViewModel>();
            SimpleIoc.Default.Register<SmartCasaViewModel>();

            //DialogService
            var dialog = new DialogService();
            SimpleIoc.Default.Register<DialogService>(() => dialog);
        }
        //Add each view's name here.
        public const string MainPage = "MainPage";
        public const string LogInPage = "LogInPage";
        public const string LogFbPage = "LogFbPage";
        public const string RegistrationPage = "RegistrationPage";
        public const string ProfilePage = "ProfilePage";
        public const string CreateSmartCasaPage = "CreateSmartCasaPage";
        public const string SmartCasaPage = "SmartCasaPage";
        
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LogInViewModel LogIn
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LogInViewModel>();
            }
        }

        public FacebookViewModel Facebook
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FacebookViewModel>();
            }
        }

        public RegistrationViewModel Registration
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RegistrationViewModel>();
            }
        }

        public ProfileViewModel Profile
        {
            get { return ServiceLocator.Current.GetInstance<ProfileViewModel>(); }
        }

        public CreateSmartCasaViewModel CreateSmartCasa
        {
            get { return ServiceLocator.Current.GetInstance<CreateSmartCasaViewModel>(); }
        }

        public SmartCasaViewModel SmartCasa
        {
            get { return ServiceLocator.Current.GetInstance<SmartCasaViewModel>(); }
        }
        
        public NavigationService NavigationService
        {
            get { return SimpleIoc.Default.GetInstance<NavigationService>(); }
        }

        public DialogService DialogService
        {
            get { return SimpleIoc.Default.GetInstance<DialogService>(); }
        }


    }
}
