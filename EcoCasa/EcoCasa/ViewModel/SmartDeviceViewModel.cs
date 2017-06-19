using System.Windows.Input;
using EcoCasa.Models;
using EcoCasa.Util;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace EcoCasa.ViewModel
{
    public class SmartDeviceViewModel : ViewModelBase
    {
        public SmartDeviceViewModel()
        {
            SmartDevice = Constants.CurrentDevice;
            IP = "Ip address: " + SmartDevice.IP;
            MAC = "MAC address: " + SmartDevice.MAC;
            DeleteDeviceCommand = new Command(async () =>
            {
                App.Database.DeleteSmartDevice(SmartDevice);
                await FirebaseUtil.DeleteSmartDevice(Constants.CurrentCasa, Constants.CurrentDevice);
                App.Locator.NavigationService.GoBack();

            });

            DeviceToggle = new Command(async () => await App.Locator.DialogService.ShowMessage("Something is going on...", "You toggled!"));
        }
        public SmartDevice SmartDevice { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public ICommand DeleteDeviceCommand { private set; get; }
        public ICommand DeviceToggle { set; get; }
    }
}