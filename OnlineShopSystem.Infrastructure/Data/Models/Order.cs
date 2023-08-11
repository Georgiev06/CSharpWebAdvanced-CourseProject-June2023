using System.ComponentModel.DataAnnotations;

namespace OnlineShopSystem.Infrastructure.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Books = new HashSet<Book>();
            this.Users = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public string UserFirstName { get; set; } = null!;

        [Required]
        public string UserLastName { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
