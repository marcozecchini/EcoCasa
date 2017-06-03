using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EcoCasa.Models
{
    [Table("SmartCasa")]
    public class SmartCasa
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string CodeCasa { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Address")] 
        public string Address { get; set; }

        //Associations
        [JsonProperty("Administrator")]
        public string Administrator { get; set; }
        
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