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

        private User _user;

        public User User
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

            var User =  await facebookServices.GetFacebookProfileAsync(accessToken);

            if (!await FirebaseUtil.HasUser(""))
            {
                var postRes = await FirebaseUtil.PostUser(User);
                //XMLUtil.SaveUserCode(postRes);
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
