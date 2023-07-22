using OnlineShopSystem.Core.Models.Category;
using OnlineShopSystem.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Core.Models.Book
{
    public class BookFormModel
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
        [StringLength(EntityValidationConstants.Book.RatingMaxLength, MinimumLength = EntityValidationConstants.Book.RatingMinLength)]
        public string Rating { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        public virtual IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
