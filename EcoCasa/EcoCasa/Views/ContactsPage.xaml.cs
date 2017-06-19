using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcoCasa.Models;
using EcoCasa.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcoCasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Constants.UserToAdd = (User) e.SelectedItem;
            if (App.Locator.Contacts.AddToCasa.CanExecute(null).Equals(true))
            {
                App.Locator.Contacts.AddToCasa.Execute(null);
            }
        }
    }
}
