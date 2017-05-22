
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace EcoCasa.ViewModel
{
    public class LogInViewModel : ViewModelBase
    {
        public LogInViewModel()
        {
            GoLogInCommand = new Command(()=> ValidateLogIn());
        }

        public ICommand GoLogInCommand { private set; get; }

        public void ValidateLogIn()
        {
            
        }
    }
}
