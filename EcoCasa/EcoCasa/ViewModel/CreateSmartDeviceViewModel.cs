using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EcoCasa.Models;
using EcoCasa.Util;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace EcoCasa.ViewModel
{
    public class CreateSmartDeviceViewModel : ViewModelBase
    {

        public CreateSmartDeviceViewModel()
        {
            ValidateSmartDevice = new Command(async () =>
            {
                if (await Validate()) {
                    App.Locator.NavigationService.SetNewRoot(Locator.ProfilePage); // se non va set new root.
                }
                
            });
        }

        public string IP { get; set; }
        public string MAC { get; set; }
        public ICommand ValidateSmartDevice { private set; get; }

        private async Task<bool> Validate()
        {
            if (IP == null || MAC == null)
            {
                await App.Locator.DialogService.ShowMessage("Fill all the fields", "Error");
                return false;
            }
            SmartDevice device = new SmartDevice();
            device.CasaCode = Constants.CurrentCasa.CodeCasa;
            device.IP = IP;
            device.MAC = MAC;
            device.CodeDevice = await FirebaseUtil.PostSmartDevice(device);

            App.Database.SaveSmartDevice(device);
            return true;
        }
    }
}