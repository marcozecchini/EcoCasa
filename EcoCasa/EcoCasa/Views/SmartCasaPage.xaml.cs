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
	public partial class SmartCasaPage : ContentPage
	{
		public SmartCasaPage ()
		{
			InitializeComponent ();
		}

	    private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        var d = e.SelectedItem;
	        Constants.CurrentDevice = (SmartDevice) d;
	        App.Locator.SmartCasa.AddDeviceCommand.Execute(null);
	    }
	}
}
