using System;
using System.Windows.Input;
using EcoCasa.Models;
using EcoCasa.ViewModel;
using Xamarin.Forms;

namespace EcoCasa.Util
{
    public class ProfileBehavior : Behavior<ListView>
    {
        protected override void OnAttachedTo(ListView list)
        {
            var a = list.SelectedItem;
            Constants.CurrentCasa = (SmartCasa)a;
            App.Locator.Profile.SetCasa.CanExecute(null);
            base.OnAttachedTo(list);
            // Perform setup
          
        }

        protected override void OnDetachingFrom(ListView list)
        {
            base.OnDetachingFrom(list);
        }

        public void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var a = e.SelectedItem;
            Constants.CurrentCasa = (SmartCasa)a;
            App.Locator.Profile.SetCasa.CanExecute(null);
        }


    }
}