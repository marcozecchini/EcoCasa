using EcoCasa.Annotations;
using Newtonsoft.Json;

namespace EcoCasa.Models
{
    public class SessionUser : User
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}