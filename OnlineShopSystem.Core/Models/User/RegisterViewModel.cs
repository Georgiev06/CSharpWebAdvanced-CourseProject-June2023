using OnlineShopSystem.Infrastructure.Common.EntityValidationConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Core.Models.User
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Compare(nameof(ConfirmPassword))]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [StringLength(EntityValidationConstants.User.FirstNameMaxLength, MinimumLength = EntityValidationConstants.User.FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(EntityValidationConstants.User.LastNameMaxLength, MinimumLength = EntityValidationConstants.User.LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(EntityValidationConstants.User.AddressMaxLength, MinimumLength = EntityValidationConstants.User.AddressMinLength)]
        public string Address { get; set; } = null!;
    }
}
