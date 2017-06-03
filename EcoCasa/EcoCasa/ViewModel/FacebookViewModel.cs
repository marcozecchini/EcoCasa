using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EcoCasa.Models;
using EcoCasa.Util;
using FacebookLogin.Util;

namespace EcoCasa.ViewModel
{
    public class FacebookViewModel : INotifyPropertyChanged
    {

        private SessionUser _user;

        public SessionUser User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public async Task<User> SetFacebookUserProfileAsync(string accessToken)
        {
            var facebookServices = new FacebookServices();

            Constants.User = User =  await facebookServices.GetFacebookProfileAsync(accessToken);
            Constants.Code = await FirebaseUtil.GetSessionUserCode(User);
            if (Constants.Code == null)
            {
                await FirebaseUtil.PostUser(User);
                App.Database.SaveSessionUser(User);

            }
            return User;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
