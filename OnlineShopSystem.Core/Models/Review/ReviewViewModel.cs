using OnlineShopSystem.Infrastructure.Common.EntityValidationConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Core.Models.Review
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public int Rating { get; set; }

        public string UserId { get; set; } = null!;

        public string UserUsername { get; set; } = null!;

        public int BookId { get; set; }
    }
}
