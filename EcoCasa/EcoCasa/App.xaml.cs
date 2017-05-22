using EcoCasa.Util;
using EcoCasa.Views;
using Xamarin.Forms;

namespace EcoCasa
{
    public partial class App : Application
    {

        private static Locator _locator;
        public static Locator Locator { get { return _locator ?? (_locator = new Locator()); } }

        public App()
        {
        
            var firstPage = new NavigationPage(new MainPage());
            Locator.NavigationService.Initialize(firstPage);
            MainPage = firstPage;

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
