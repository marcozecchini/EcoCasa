using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EcoCasa.Models
{
    [Table("SmartCasa")]
    public class SmartCasa
    {
        
        public int Id { get; set; }
        [PrimaryKey]
        public string CodeCasa { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Address")] 
        public string Address { get; set; }
        //to check if it's insert or update
        public bool Update { get; set; }
        //Associations
        [JsonProperty("Ad_Email")]
        public string Administrator { get; set; }

        [JsonProperty("Device")]
        public string Device { get; set; }

        [JsonProperty("CodeUsers"), Column("UserEmail")]
        public string UserEmail { get; set; }
    }

    [Table("Address")]
    public class Address
    {
        [JsonProperty("Street")]
        [PrimaryKey]
        public string Street { get; set; }
        [JsonProperty("CAP")]
        [PrimaryKey]
        public string CAP { get; set; }
        [JsonProperty("City")]
        [PrimaryKey]
        public string City { get; set; }
        [JsonProperty("Nation")]
        [PrimaryKey]
        public string Nation { get; set; }

        public Address()
        {
            
        }

        public Address(string Street, string City, string CAP, string Nation)
        {
            this.Street = Street;
            this.CAP = CAP;
            this.City = City;
            this.Nation = Nation;

        }
    }
}