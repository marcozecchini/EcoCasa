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
        public static string Scope = "";
        public static string AuthorizeUrl = "https://m.facebook.com/dialog/oauth/";
       
        // Set this property to the location the user will be redirected too after successfully authenticating
        public static string RedirectUrl = "http://www.facebook.com/connect/login_success.html";
    }
}
