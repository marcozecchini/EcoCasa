using Newtonsoft.Json;
using SQLite;

namespace EcoCasa.DB.Associations
{
    public class SmartCasaUserAssiociation
    {
        [PrimaryKey, AutoIncrement]
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Email")]
        [Column("Email")]
        public string Email { get; set; }

        [JsonProperty("CodeCasa")]
        public string CodeCasa { get; set; }

    }
}