using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EcoCasa.Models;
using EcoCasa.Util;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace EcoCasa.ViewModel
{
    public class RegistrationViewModel : ViewModelBase
    {
        
        public RegistrationViewModel ()
        {
            SendCommand = new Command(async () =>
            {
                var res = await ValidateRegistration();
                 if (res) App.Locator.NavigationService.SetNewRoot(Locator.ProfilePage);
            }); 
        }

        public ICommand SendCommand { private set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string Email { set; get; }

        public string ConfirmEmail { set; get; }

        public string Password { set; get; }

        public string ConfirmPassword { set; get; }

        private async Task<bool> ValidateRegistration()
        {
            SessionUser User = new SessionUser();
            User.Name = FirstName + " " + LastName;
            if (FirstName == null || LastName == null || Email == null || ConfirmEmail == null || Password == null ||
                ConfirmPassword == null)
            {
                await App.Locator.DialogService.ShowMessage("Fill all fields", "Error");
                return false;
            }

            if (Email.Equals(ConfirmEmail) && Regex.Match(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                if (Password.Equals(ConfirmPassword))
                {
                    User.Email = Email;
                    if (await FirebaseUtil.HasUser(await FirebaseUtil.GetSessionUserCode(User)))
                    {
                        await App.Locator.DialogService.ShowMessageBox(
                            "Email already present in the database", "Something wrong");
                        return false;
                    }
                    //Encrypting password
                    User.Password = Password; //Cryptor.EncryptAes(Password, Constants.pass, Constants.salt);

                    Constants.Code = await FirebaseUtil.PostUser(User);
                    Constants.User = User;
                    App.Database.SaveSessionUser(Constants.User);

                    return true;

                }
                else
                {
                    await App.Locator.DialogService.ShowMessageBox("Passwords don't match", "Something wrong");
                    return false;
                }
            }
            else
            {
                await App.Locator.DialogService.ShowMessageBox("Emails don't match or email isn't well written",
                    "Something wrong");
                return false;
            }
        }
        
    }
}
