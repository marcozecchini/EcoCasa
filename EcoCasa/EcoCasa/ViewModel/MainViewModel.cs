using System;
using System.Windows.Input;
using EcoCasa.Util;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

using Xamarin.Forms;


namespace EcoCasa.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        
        public MainViewModel()
        {
            LogInCommand = new Command( ()=> App.Locator.NavigationService.NavigateTo(Locator.LogInPage) );
            RegistrationCommand = new Command(()=> App.Locator.NavigationService.NavigateTo(Locator.RegistrationPage));
            LogInWithFB = new Command(() => LogIn());
        }

        public ICommand LogInCommand { private set; get; }
        public ICommand RegistrationCommand { private set; get; }
        public ICommand LogInWithFB { private set; get; }

        public void LogIn()
        {
            
        }

    }

  
}
