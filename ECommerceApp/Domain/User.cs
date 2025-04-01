using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        //public string PasswordHash { get; set; }
        //[Required]
        public string[] Role { get; set; }

        public virtual List<Address> Address { get; set; } = new List<Address>();

        public virtual Cart Cart { get; set; }
    }
}
