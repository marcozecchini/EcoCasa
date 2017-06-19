using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SQLite;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EcoCasa.Models
{
    public class SmartDevice
    {
        [PrimaryKey]
        public string CodeDevice { get; set; }
        [JsonProperty("MAC")]
        public string MAC { get; set; }
        [JsonProperty("IP")]
        public string IP { get; set; }
        [JsonProperty("CasaCode")]
        public string CasaCode { get; set; }
        public bool State = false;
        public bool Update = false;
    }
}