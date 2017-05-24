using EcoCasa.Models;
using Microsoft.WindowsAzure.MobileServices;


namespace EcoCasa.Util
{   
    
    public class DataManagement
    {
        public static MobileServiceClient MobileService;

        public static IMobileServiceTable<User> UserTable;

        private DataManagement()
        {
            
            MobileService = new MobileServiceClient(
                "https://ecocasa.azurewebsites.net"
            );
            UserTable = MobileService.GetTable<User>();
        }

        public static void Initialize()
        {
            new DataManagement();
        }
    }
}
