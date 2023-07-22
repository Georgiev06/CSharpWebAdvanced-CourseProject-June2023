using OnlineShopSystem.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Core.Models.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(EntityValidationConstants.Category.NameMaxLength, MinimumLength = EntityValidationConstants.Category.NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
