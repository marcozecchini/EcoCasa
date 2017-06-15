using EcoCasa.Models;
using EcoCasa.Util;
using GalaSoft.MvvmLight;

namespace EcoCasa.ViewModel
{
    public class SmartCasaViewModel : ViewModelBase
    {
        public SmartCasaViewModel()
        {
            Casa = Constants.CurrentCasa;
        }

        public SmartCasa Casa { get; private set; }
    }
}
