
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EcoCasa.Models;
using EcoCasa.Util;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace EcoCasa.ViewModel
{
    public class LogInViewModel : ViewModelBase
    {
        public LogInViewModel()
        {
            GoLogInCommand = new Command(async () =>
            {
                var res = await ValidateLogIn();
                await FirebaseUtil.UpdateSmartCasas();
                if (res) App.Locator.NavigationService.SetNewRoot(Locator.ProfilePage);
            });
        }

        public ICommand GoLogInCommand { private set; get; }

        public string Email { set; get; }
        public string Password { set; get; }

        private async Task<bool> ValidateLogIn()
        {
            if (Email == null || Password == null || !Regex.Match(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                    .Success)
            {
                await App.Locator.DialogService.ShowMessageBox("Fill better all the fields", "Error");
                return false;
            }

            
            User temp_user = new User();
            temp_user.Email = Email;
            temp_user.Password = Password;

            var code = await FirebaseUtil.GetUserCodeWithPassword(temp_user);
            if (code != null)
            {
                User u = await FirebaseUtil.GetUserDate(code);
                //salvo tutti in constants.user
                Constants.User = new SessionUser();
                Constants.User.Password = u.Password;
                Constants.User.Email = u.Email;
                Constants.User.Name = u.Name;
                //Salvo user.id e lo metto nel database.
                Constants.User.ID = App.Database.SaveSessionUser(Constants.User);
                App.Database.SaveSessionUser(Constants.User);
                Constants.Code = code;
                return true;
            }
            else
            {
                await App.Locator.DialogService.ShowMessageBox("The asked user isn't in the database", "Attention");
                return false;
            }
        }
    }
}
