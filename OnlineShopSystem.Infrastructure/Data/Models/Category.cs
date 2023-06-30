using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Infrastructure.Common;

namespace OnlineShopSystem.Infrastructure.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidationConstants.Category.NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required] 
        public virtual ICollection<Book> Books { get; set; }
    }
}
