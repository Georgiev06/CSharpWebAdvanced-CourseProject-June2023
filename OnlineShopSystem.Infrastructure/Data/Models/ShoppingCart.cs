using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopSystem.Infrastructure.Data.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.Books = new HashSet<Book>();
        }

        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [Required]
        public virtual ICollection<Book> Books { get; set; }
    }
}
