using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Infrastructure.Common.EntityValidationConstants;

namespace OnlineShopSystem.Infrastructure.Data.Models
{
    public class Book
    {
        public Book()
        {
            this.Reviews = new HashSet<Review>();
            this.UsersBooks = new HashSet<UserBook>();
            this.ShoppingCarts = new HashSet<ShoppingCart>();
            this.Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidationConstants.Book.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(EntityValidationConstants.Book.AuthorNameMaxLength)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(EntityValidationConstants.Book.DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(EntityValidationConstants.Book.ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required] 
        public decimal Price { get; set; }

        [Required]
        public decimal Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<ShoppingCart>? ShoppingCarts { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<UserBook> UsersBooks { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
