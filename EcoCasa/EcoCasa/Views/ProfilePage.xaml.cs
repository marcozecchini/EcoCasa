using EcoCasa.Models;
using EcoCasa.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcoCasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var a = e.SelectedItem;
            e = null;
            sender = null;
            Constants.CurrentCasa = (SmartCasa) a;
            App.Locator.Profile.SetCasa.Execute(null);
        }

    }
}
