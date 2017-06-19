using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EcoCasa.DB.Associations;
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

        public static async Task<JObject> GetSmartCasaDB()
        {
            var req = DB_URL + "/SmartCasa.json?auth=" + AUTH_TOKEN;
            var httpClient = new HttpClient();
            JObject JSON = null;

            var DBJson = await httpClient.GetStringAsync(req);
            if (!DBJson.Equals("null")) JSON = JObject.Parse(DBJson);

            return JSON;
        }

        public static async Task<String> GetSessionUserCode(SessionUser user)
        {
            JObject JSON = await GetUserDB();
            if (JSON == null) return null;
            foreach (var juser in JSON)
            {
                if (user.Email.Equals(juser.Value["Email"].Value<String>())) return juser.Key;
            }

            return null;

        }

        public static async Task<String> GetUserCode(User user)
        {
            JObject JSON = await GetUserDB();
            if (JSON == null) return null;
            foreach (var juser in JSON)
            {
                if (user.Email.Equals(juser.Value["Email"].Value<String>())) return juser.Key;
            }
            
            return null;

        }

        public static async Task<string> GetSmartCasaCode(SmartCasa casa)
        {
            JObject JSON = await GetSmartCasaDB();
            if (JSON == null) return null;
            foreach (var juser in JSON)
            {
                if (casa.Address.Equals(juser.Value["Address"].Value<String>()) && casa.Administrator.Equals(juser.Value["Ad_Email"].Value<string>())) return juser.Key;
            }

            return null;
        }

        

        public static async Task<String> GetUserCodeWithPassword(User user)
        {
            JObject JSON = await GetUserDB();
            
            if (JSON == null) return null;
            foreach (var juser in JSON)
            {
                try
                {
                    var json_string = juser.Value["Password"].Value<string>();
                    //var decrypted = Cryptor.DecryptAes(Convert.FromBase64String(json_string), Constants.pass, Constants.salt);
                    if (user.Email.Equals(juser.Value["Email"].Value<String>()) &&
                        user.Password.Equals(json_string)) return juser.Key;
                } catch { }
                
            }

            return null;

        }

        public static async Task<User> GetUserDate(String id)
        {
            var req = DB_URL + "/User.json?auth=" + AUTH_TOKEN +"&orderBy=\"$key\"&startAt=\""+id+"\"&endAt=\""+id+"\"";
            var httpClient = new HttpClient();

            var DBJson = await httpClient.GetStringAsync(req);
            var removedString = removeCode(id, DBJson);
            var User = JsonConvert.DeserializeObject<User>(removedString);

            return User;
        }



        public static async Task<SmartCasa> GetSmartCasaDate(string id)
        {
            var req = DB_URL + "/SmartCasa.json?auth=" + AUTH_TOKEN + "&orderBy=\"$key\"&startAt=\"" + id + "\"&endAt=\"" + id + "\"";
            var httpClient = new HttpClient();
            var DBJson = await httpClient.GetStringAsync(req);
            var removedString = removeCode(id, DBJson);
            var casa = JsonConvert.DeserializeObject<SmartCasa>(removedString);

            return casa;
        }

        public static async Task<bool> HasUser(String id)
        {
            var user = await GetUserDate(id);
            
            if (user.Email == null) return false;
            return true;
        }

        public static async Task<bool> HasSmartCasa(String id)
        {
            var casa = await GetSmartCasaDate(id);

            if (casa == null || casa.Address == null || casa.Administrator == null) return false;
            return true;
        }

        public static async Task<String> PostUser(User user)
        {
            var req = DB_URL + "/User.json?auth=" + AUTH_TOKEN;
            JsonConvert.SerializeObject(user);
            var content = new StringContent(JsonConvert.SerializeObject(user));
            var httpClient = new HttpClient();

            var httpResponse = await httpClient.PostAsync(req, content);
            var responseData = httpResponse.Content.ReadAsStringAsync().Result;
            var resultData = exctractCode(responseData);
            return resultData;
        }

        public static async Task<String> PostSmartCasa(SmartCasa casa)
        {
            var req = DB_URL + "/SmartCasa.json?auth=" + AUTH_TOKEN;
            var content = new StringContent(JsonConvert.SerializeObject(casa));
            var httpClient = new HttpClient();

            var httpResponse = await httpClient.PostAsync(req, content);
            var responseData = httpResponse.Content.ReadAsStringAsync().Result;
            var resultData = exctractCode(responseData);
            return resultData;
        }

        public static async Task<bool> DeleteSmartCasa(SmartCasa casa)
        {
            var req = DB_URL + "/SmartCasa/" + casa.CodeCasa + ".json?auth=" + AUTH_TOKEN;
//            var content = new StringContent(JsonConvert.SerializeObject(casa));
            var httpClient = new HttpClient();

            var httpResponse = await httpClient.DeleteAsync(req);
            var responseData = httpResponse.StatusCode;

            if (responseData == HttpStatusCode.OK) return true;
            else return false;

        }

        public static async Task<bool> addContacts(User Contact, string code)
        {
            var req = DB_URL + "/User/" + Constants.Code + "/Users/" + code + ".json?auth=" + AUTH_TOKEN;            
            var content = new StringContent(JsonConvert.SerializeObject(Contact));
            var httpClient = new HttpClient();

            var httpResponse = await httpClient.PutAsync(req, content);
            var responseData = httpResponse.StatusCode;

            if (responseData == HttpStatusCode.OK) return true;
            else return false;
        }

        public static async Task<bool> AddUserToSmartCasa(string Email)
        {
            var mail = Email.Replace(".", "-");
            mail = mail.Replace("@", "-");
            var req = DB_URL + "/SmartCasa/" + Constants.CurrentCasa.CodeCasa + "/Users/" + mail + ".json?auth=" + AUTH_TOKEN;
            var content = new StringContent(JsonConvert.SerializeObject(Email));
            var httpClient = new HttpClient();

            var httpResponse = await httpClient.PutAsync(req, content);
            var responseData = httpResponse.StatusCode;

            if (responseData == HttpStatusCode.OK) return true;
            else return false;
        }

        public static async Task<bool> UpdateSmartCasas()
        {
            try
            {
                JObject JSON = await FirebaseUtil.GetSmartCasaDB();
                if (JSON == null)
                {
                    List<SmartCasa> lista = App.Database.GetCasas(Constants.User);
                    App.Database.DeleteAllSmartCasa();
                }
                else
                {
                    await UpdateAsAdministrator(JSON);
                    await UpdateCasasAsUser(JSON);
                }

                
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static async Task<bool> UpdateAsAdministrator(JObject JSON)
        {
            try
            {
                List<string> smartKey = new List<string>();
                foreach (var jcasa in JSON)
                {
                    if (jcasa.Value["Ad_Email"].Value<string>().Equals(Constants.User.Email)) smartKey.Add(jcasa.Key);
                }
                List<SmartCasa> smartcasa = new List<SmartCasa>();

                foreach (var code in smartKey)
                {
                    SmartCasa casaDate = await FirebaseUtil.GetSmartCasaDate(code);
                    casaDate.CodeCasa = code;
                    smartcasa.Add(casaDate);
                }

                foreach (var casa in smartcasa)
                {
                    var res = App.Database.GetSmartCasa(casa.CodeCasa);
                    if (res == null)
                    {
                        App.Database.SaveSmartCasa(casa);
                        
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }


        private static async Task<bool> UpdateCasasAsUser(JObject JSON)
        {
            

            //get all the key of the smartcasa where there is this user.
            List<string> smartKey = new List<string>();
            try
            {
                foreach (var jcasa in JSON)
                {
                    try
                    {
                        List<String> mails = await getEmailUsersInSmartCasa(jcasa.Key);
                        foreach (var mail in mails)
                        {
                            if (mail.Equals(Constants.User.Email)) smartKey.Add(jcasa.Key);
                        }

                    }
                    catch
                    {
                    }

                }


                List<SmartCasa> smartcasa = new List<SmartCasa>();

                foreach (var code in smartKey)
                {
                    SmartCasa casaDate = await FirebaseUtil.GetSmartCasaDate(code);
                    casaDate.CodeCasa = code;
                    smartcasa.Add(casaDate);
                }

                foreach (var casa in smartcasa)
                {
                    var res = App.Database.GetSmartCasa(casa.CodeCasa);
                    if (res == null)
                    {
                        casa.Id = App.Database.SaveSmartCasa(casa);
                        App.Database.SaveSmartCasa(casa);

                        SmartCasaUserAssiociation association = new SmartCasaUserAssiociation();
                        association.ID = ++Constants.AssociationSmartCasaUserSize;
                        association.Email = Constants.User.Email;
                        association.CodeCasa = casa.CodeCasa;
                        App.Database.SaveSmartCasaUserAssociation(association);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }


        private static async Task<List<String>> getEmailUsersInSmartCasa(string idCasa)
        {
            List<string> res = new List<string>();
            var req = DB_URL + "/SmartCasa.json?auth=" + AUTH_TOKEN + "&orderBy=\"$key\"&startAt=\"" + idCasa + "\"&endAt=\"" + idCasa + "\"";
            var httpClient = new HttpClient();
            var DBJson = await httpClient.GetStringAsync(req);
            JObject JSON = JObject.Parse(DBJson);
            var Users = JSON.First.Last["Users"]; //For getting the users
            foreach (var user in Users)
            {
                res.Add(user.First.Value<string>());
            }
            return res;
        }

        private static string removeCode(string code, string RemoveFromHere)
        {
            var stringRes = "";
            if (RemoveFromHere.Contains("{\"" + code + "\":"))
            {
                stringRes = RemoveFromHere.Replace("{\"" + code + "\":", "");
                stringRes = stringRes.Replace("}}", "}");
            }

            return stringRes;
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
