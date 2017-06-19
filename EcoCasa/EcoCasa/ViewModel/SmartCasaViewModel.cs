using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EcoCasa.Models;
using EcoCasa.Util;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace EcoCasa.ViewModel
{
    public class SmartCasaViewModel : ViewModelBase
    {
        public SmartCasaViewModel()
        {
            Casa = Constants.CurrentCasa;
            Administrator = IsAdministrator();
            
            Device = new ObservableCollection<SmartDevice>();
            DeviceEnumerator = App.Database.GetSmartDevicesOfSmartCasa(Casa.CodeCasa);
            foreach (var c in DeviceEnumerator) Device.Add(c);

            AddNewToSmartCasaCommand = new Command(() =>
           {
                Constants.SmartCasaBefore = true;
                App.Locator.NavigationService.NavigateTo(Locator.ContactsPage);

           });

            DeleteCommand = new Command( async () => { 

                await FirebaseUtil.DeleteSmartCasa(Casa);
                App.Database.DeleteSmartCasa(Casa);
                App.Locator.NavigationService.SetNewRoot(Locator.ProfilePage);
            
            });

            AddDeviceCommand = new Command(() => App.Locator.NavigationService.NavigateTo(Locator.CreateSmartDevicePage));

            DeviceCommand = new Command(() => App.Locator.NavigationService.NavigateTo(Locator.SmartDevicePage));
        }

        private bool IsAdministrator()
        {
            return Casa.Administrator.Equals(Constants.User.Email);
        }

        public List<SmartDevice> DeviceEnumerator { get; set; }
        public ObservableCollection<SmartDevice> Device { get; set; }

        public bool Administrator { get; private set; }
        public SmartCasa Casa { get; private set; }
        public ICommand AddNewToSmartCasaCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand AddDeviceCommand { private set; get; }
        public ICommand DeviceCommand { set; get; }
    }
}
