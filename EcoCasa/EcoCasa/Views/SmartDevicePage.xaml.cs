using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcoCasa.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcoCasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SmartDevicePage : ContentPage
    {
        public SmartDevicePage()
        {
            InitializeComponent();
        }

        private void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                Constants.CurrentDevice.Update = true;
                App.Locator.SmartDevice.DeviceToggle.Execute(null);
            }
            else Constants.CurrentCasa.Update = false;
        }
    }
}
