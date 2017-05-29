using System;
using System.Net.Http;
using System.Threading.Tasks;
using EcoCasa.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace EcoCasa.Util
{
    public class FirebaseUtil
    {
        private const String DB_URL = "https://ecocasa-b9106.firebaseio.com";
        private const String AUTH_TOKEN = "4p61g4H2mT0ko7YH0RzUreSguVrbyxGzzTsca7Cx";

        public static async Task<JObject> GetUserDB()
        {
            var req = DB_URL + "/User.json?auth=" + AUTH_TOKEN;
            var httpClient = new HttpClient();
            JObject JSON = null;

            var DBJson = await httpClient.GetStringAsync(req);
            if (!DBJson.Equals("null")) JSON  = JObject.Parse(DBJson);
            
            return JSON;
        }

        public static async Task<String> GetUserCode(User user)
        {
            JObject JSON = await GetUserDB();
            if (JSON == null) return null;
            foreach (var juser in JSON)
            {
                if (user.Email.Equals(juser.Value["Email"].Value<String>()) && user.Name.Equals(juser.Value["Name"].Value<String>())) return juser.Key;
            }
            
            return null;

        }

        public static async Task<User> GetUserDate(String id)
        {
            var req = DB_URL + "/User.json?auth=" + AUTH_TOKEN +"&orderBy=\"$key\"&startAt=\""+id+"\"&endAt=\""+id+"\"";
            var httpClient = new HttpClient();

            var DBJson = await httpClient.GetStringAsync(req);
            var User = JsonConvert.DeserializeObject<User>(DBJson);

            return User;
        }

        public static async Task<bool> HasUser(String id)
        {
            var user = await GetUserDate(id);
            
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
