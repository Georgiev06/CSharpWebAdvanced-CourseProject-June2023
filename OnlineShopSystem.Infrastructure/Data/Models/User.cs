using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineShopSystem.Infrastructure.Common;

namespace OnlineShopSystem.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.UsersBooks = new HashSet<UserBook>();
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

        [Required]
        public virtual ICollection<UserBook> UsersBooks { get; set; }

        [Required]
        public int ShoppingCartId { get; set; }

        [ForeignKey(nameof(ShoppingCartId))]
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
