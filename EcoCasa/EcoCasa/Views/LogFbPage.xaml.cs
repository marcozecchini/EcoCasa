using System.Threading.Tasks;
using EcoCasa.Util;
using EcoCasa.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcoCasa.Views
{
    
    public partial class LogFbPage : ContentPage
    {
        
        public  LogFbPage()
        {
            InitializeComponent();
            DoLog();
            
        }

        private async void DoLog()
        {
            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + Constants.ClientId
                + "&display=popup&scope=email&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;
            
            Content = webView;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = new FacebookViewModel();

                var user = await vm.SetFacebookUserProfileAsync(accessToken);
                //TODO Navigation to showuserprofile
                //Navigate to con task.result che è un user

            }
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                
                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
    }
}
