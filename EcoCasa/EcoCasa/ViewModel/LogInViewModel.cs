
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace EcoCasa.ViewModel
{
    public class LogInViewModel : ViewModelBase
    {
        public LogInViewModel()
        {
            //GoLogInCommand = new Command(()=>true);
        }

        public ICommand GoLogInCommand { private set; get; }
    }
}
