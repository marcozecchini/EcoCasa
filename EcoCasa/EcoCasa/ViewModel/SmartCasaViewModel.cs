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
        }

        private bool IsAdministrator()
        {
            return Casa.Administrator.Equals(Constants.User.Email);
        }

        public bool Administrator { get; private set; }
        public SmartCasa Casa { get; private set; }
        public ICommand AddNewToSmartCasaCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
    }
}
