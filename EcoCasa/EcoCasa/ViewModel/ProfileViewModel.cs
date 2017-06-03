using System.Collections.Generic;
using System.Windows.Input;
using EcoCasa.DB;
using EcoCasa.Models;
using EcoCasa.Util;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace EcoCasa.ViewModel
{
    public class ProfileViewModel : ViewModelBase
    {
        public ProfileViewModel()
        {
            Email = Constants.User.Email;
            Name = Constants.User.Name;
            Casa = App.Database.GetCasas();

            LogOutCommand = new Command(() =>
            {
                var del = App.Database.DeleteSessionUser(Constants.User);
                Constants.User = null;
                Constants.Code = "";
                App.Locator.NavigationService.SetNewRoot(Locator.MainPage);
            });
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public List<SmartCasa> Casa { get; set; }

        public ICommand LogOutCommand { private set; get; }
    }
}