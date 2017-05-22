namespace EcoCasa.Models
{
    public class User
    {
        public string Name { get; set; }
        public Picture Picture { get; set; }
        private string Password { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }



}
