using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceApp.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        [ForeignKey("User")]
        public  int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        

    }
}
