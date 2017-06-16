using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EcoCasa.Models;
using EcoCasa.Util;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace EcoCasa.ViewModel
{
    public class ContactsViewModel : ViewModelBase
    {

        public ContactsViewModel()
        {
            UserEnumerator = App.Database.FindAllUsers();
            Contacts = new ObservableCollection<User>();
            ValidateUser = new Command( async () => await Validate() );
            foreach (var u in  UserEnumerator)  Contacts.Add(u);
            
        }

        public ICommand ValidateUser { private set; get; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<User> UserEnumerator { get; set; }
        public ObservableCollection<User> Contacts { get; set; }

        private async Task Validate()
        {
            if (Email == null)
            {
                await App.Locator.DialogService.ShowMessageBox("Fill the fields", "Error");
                return;
            }

            var user = new User();
            user.Email = Email;
            user.Name = Name;

            var codeUser = await FirebaseUtil.GetUserCode(user);
            if (codeUser==null)
            {
                await App.Locator.DialogService.ShowMessageBox("User not registered", "Error");
                return;
            }

            App.Database.SaveUser(user);
            await FirebaseUtil.addContacts(user, codeUser);
            
        }
    }
}