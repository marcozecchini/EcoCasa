using Newtonsoft.Json;

namespace EcoCasa.Models
{
    public class User
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        public Picture Picture { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        public string Telefono { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }



}
