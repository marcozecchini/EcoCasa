﻿using System.Threading.Tasks;
using System.Windows.Input;
using EcoCasa.Models;
using EcoCasa.Util;
using GalaSoft.MvvmLight;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EcoCasa.ViewModel
{
    public class CreateSmartCasaViewModel : ViewModelBase
    {
        public CreateSmartCasaViewModel ()
        {
            ValidateSmartCasa = new Command(async () =>
            {
                if (await Validate()) App.Locator.NavigationService.SetNewRoot(Locator.ProfilePage);
            });
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public ICommand ValidateSmartCasa { private set; get; }

        private async Task<bool> Validate()
        {
            if (Name == null || Address == null)
            {
                await App.Locator.DialogService.ShowMessage("Fill all the fields", "Error");
                return false;
            }
            
            SmartCasa casa = new SmartCasa();
            casa.Address = Address;
            casa.Name = Name;
            casa.Administrator = Constants.User.Email;

            if (await FirebaseUtil.HasSmartCasa(await FirebaseUtil.GetSmartCasaCode(casa)))
            {
                await App.Locator.DialogService.ShowMessageBox("Casa already in the db", "Error");
                return false;
            }
            await FirebaseUtil.PostSmartCasa(casa);
            App.Database.SaveSmartCasa(casa);
            return true;
        }
    }
}