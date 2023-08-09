using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Infrastructure.Common.EntityValidationConstants;

namespace OnlineShopSystem.Core.Models.Review
{
    public class AddReviewViewModel
    {
        [Required]
        [StringLength(EntityValidationConstants.Review.CommentMaxLength, MinimumLength = EntityValidationConstants.Review.CommentMinLength)]
        public string Comment { get; set; } = null!;

        [Range(EntityValidationConstants.Review.RatingMinLength, EntityValidationConstants.Review.RatingMaxLength)]
        public int Rating { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int BookId { get; set; }
    }
}
