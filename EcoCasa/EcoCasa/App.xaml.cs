using EcoCasa.Annotations;
using EcoCasa.DB;
using EcoCasa.Util;
using EcoCasa.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EcoCasa
{
    public partial class App : Application
    {

        private static Locator _locator;
        public static Locator Locator { get { return _locator ?? (_locator = new Locator()); } }

        static EcoCasaDatabase _database;
        public App()
        {
            NavigationPage firstPage;
            if (Database.CountSessionUser() == 0)
            {
                firstPage = new NavigationPage(new MainPage());
            }
            else
            {
                Constants.User = Database.FindSessionUser();
                firstPage = new NavigationPage(new ProfilePage());
            }
                

            Locator.NavigationService.Initialize(firstPage);
            Locator.DialogService.Initialize(firstPage);
            MainPage = firstPage;
        }

        public static EcoCasaDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new EcoCasaDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("EcoCasa.db"));
                }
                return _database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
