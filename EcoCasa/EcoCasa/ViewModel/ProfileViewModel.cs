using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Nome = Constants.User.Name;
            Casa = new ObservableCollection<SmartCasa>();
            CasaEnumerator = App.Database.GetCasas(Constants.User);
            foreach (var c in CasaEnumerator) Casa.Add(c);

            LogOutCommand = new Command(() =>
            {
                var del = App.Database.DeleteSessionUser(Constants.User);
                Constants.User = null;
                Constants.Code = "";
                App.Locator.NavigationService.SetNewRoot(Locator.MainPage);
            });

            CreateSmartCasaCommand = new Command(() => App.Locator.NavigationService.NavigateTo(Locator.CreateSmartCasaPage));
            SetCasa = new Command( () =>
            {
                //Constants.CurrentCasa.Name = (string)name;
                App.Locator.NavigationService.NavigateTo(Locator.SmartCasaPage);

            });

            Contacts = new Command(() => App.Locator.NavigationService.NavigateTo(Locator.ContactsPage));
            
        }
        

        public string Nome { get; set; }
        public string Email { get; set; }
        public List<SmartCasa> CasaEnumerator { get; set; }
        public ObservableCollection<SmartCasa> Casa { get; set; }
        

        public ICommand LogOutCommand { private set; get; }
        public ICommand CreateSmartCasaCommand { private set; get; }
        public ICommand Contacts { private set; get; }
        public ICommand SetCasa { set; get; }
        
    }

    
}