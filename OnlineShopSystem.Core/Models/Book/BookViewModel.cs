using OnlineShopSystem.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Core.Models.Book
{
    public class BookViewModel
    {
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
        public string Rating { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
