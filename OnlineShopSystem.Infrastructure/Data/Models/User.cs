using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using OnlineShopSystem.Infrastructure.Common.EntityValidationConstants;

namespace OnlineShopSystem.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.UsersBooks = new HashSet<UserBook>();
            this.Orders = new HashSet<Order>();
        }

        [Required]
        [MaxLength(EntityValidationConstants.User.FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(EntityValidationConstants.User.LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(EntityValidationConstants.User.AddressMaxLength)]
        public string Address { get; set; } = null!;

        public bool IsAdmin { get; set; }

        public int? ShoppingCartId { get; set; }

        public virtual ShoppingCart? ShoppingCart { get; set; }

        public virtual ICollection<UserBook>? UsersBooks { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
