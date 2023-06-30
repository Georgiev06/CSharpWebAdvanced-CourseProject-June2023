using OnlineShopSystem.Infrastructure.Common;
using OnlineShopSystem.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Core.Models.Category;

namespace OnlineShopSystem.Core.Models.Book
{
    public class AddBookViewModel
    {
        [Required]
        [StringLength(EntityValidationConstants.Book.TitleMaxLength, MinimumLength = EntityValidationConstants.Book.TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(EntityValidationConstants.Book.AuthorNameMaxLength, MinimumLength = EntityValidationConstants.Book.AuthorNameMinLength)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(EntityValidationConstants.Book.DescriptionMaxLength, MinimumLength = EntityValidationConstants.Book.DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(EntityValidationConstants.Book.ImageUrlMaxLength, MinimumLength = EntityValidationConstants.Book.ImageUrlMinLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        //[ForeignKey(nameof(CategoryId))]
        //public virtual Category Category { get; set; } = null!;

        //[Required]
        //public virtual ICollection<Review> Reviews { get; set; }

        //[Required]
        //public virtual ICollection<OrderBook> OrdersBooks { get; set; }
    }
}
