using System;
using System.Globalization;
using EcoCasa.Models;

namespace EcoCasa.Util
{
    public class Constants
    {
        public static string AppName = "EcoCasa";

        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string ClientId = "1194101617385824";
        public static string ClientSecret = "65bd34723296d5a88c47416e374dd118";

        // These values do not need changing
        public static string Scope = ""; //"public_profile+email";
        public static Uri AuthorizeUrl = new Uri("https://m.facebook.com/dialog/oauth/");
       
        // Set this property to the location the user will be redirected too after successfully authenticating
        public static Uri RedirectUrl = new Uri("http://www.facebook.com/connect/login_success.html");

        //public User DataCode 
        public static String Code;
        //public User Data
        public static SessionUser User;

        //key for encrypting
        public static String pass = "pin(C&3F.931\"lsh";
        //salt for encrypting
        public static byte[] salt = Cryptor.CreateSalt(16);

        //SmartCasa to visualize
        public static SmartCasa CurrentCasa;
    }
}
