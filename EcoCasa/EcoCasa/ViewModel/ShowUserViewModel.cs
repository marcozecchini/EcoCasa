using EcoCasa.Models;
using GalaSoft.MvvmLight;

namespace EcoCasa.ViewModel
{
    public class ShowUserViewModel:ViewModelBase
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                if (_user.Equals(value)) return;
                _user = User;
                RaisePropertyChanged(() => User);
            }

        }


    }
}
