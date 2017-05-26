using System;
using System.Net.Http;
using System.Threading.Tasks;
using EcoCasa.Models;
using Newtonsoft.Json;

namespace EcoCasa.Util
{
    public class FirebaseUtil
    {
        private const String DB_URL = "https://ecocasa-b9106.firebaseio.com";
        private const String AUTH_TOKEN = "4p61g4H2mT0ko7YH0RzUreSguVrbyxGzzTsca7Cx";

        public static async Task<Object> GetUserDB()
        {
            var req = DB_URL + "/User.json?auth=" + AUTH_TOKEN;
            var httpClient = new HttpClient();

            var DBJson = await httpClient.GetStringAsync(req);
            var DBCollection = JsonConvert.DeserializeObject(DBJson);

            return DBCollection;
        }

        public static async Task<User> GetUser(String id)
        {
            var req = DB_URL + "/User.json?auth=" + AUTH_TOKEN +"&orderBy=\"$key\"&startAt=\""+id+"\"&endAt=\""+id+"\"";
            var httpClient = new HttpClient();

            var DBJson = await httpClient.GetStringAsync(req);
            var User = JsonConvert.DeserializeObject<User>(DBJson);

            return User;
        }

        public static async Task<bool> HasUser(String id)
        {
            var user = await GetUser(id);
            
            if (user.Email == null) return false;
            return true;
        }

        public static async Task<String> PostUser(User user)
        {
            var req = DB_URL + "/User.json?auth=" + AUTH_TOKEN;
            var content = new StringContent(JsonConvert.SerializeObject(user));
            var httpClient = new HttpClient();

            var httpResponse = await httpClient.PostAsync(req, content);
            var responseData = httpResponse.Content.ReadAsStringAsync().Result;
            var resultData = exctractCode(responseData);
            return resultData;
        }

        private static string exctractCode(String response)
        {
            var stringRes = "";
            if (response.Contains("{\"name\":\""))
            {
                response = response.Replace("{\"name\":\"", "");
                stringRes = response.Remove(response.IndexOf("\"}"));
            }
            return stringRes;
        }

        

       
    }
}
