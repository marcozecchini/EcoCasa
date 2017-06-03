using System.Net.Http;
using System.Threading.Tasks;
using EcoCasa.Models;
using EcoCasa.Util;
using Newtonsoft.Json;

namespace FacebookLogin.Util
{
    public class FacebookServices
    {

        public async Task<SessionUser> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/me?fields=name,email&access_token="
                + accessToken;

            var httpClient = new HttpClient();
           
            var userJson = await httpClient.GetStringAsync(requestUrl);
            
            var facebookProfile = JsonConvert.DeserializeObject<SessionUser>(userJson);
            
            return facebookProfile;
        }
    }
}
