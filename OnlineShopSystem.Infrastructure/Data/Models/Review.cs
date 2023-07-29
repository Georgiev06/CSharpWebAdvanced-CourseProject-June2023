using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Infrastructure.Common.EntityValidationConstants;

namespace OnlineShopSystem.Infrastructure.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidationConstants.Review.RatingMaxLength)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(EntityValidationConstants.Review.CommentMaxLength)]
        public string Comment { get; set; } = null!;

        [Required]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;
    }
}
