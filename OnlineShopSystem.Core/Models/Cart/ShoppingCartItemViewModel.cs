using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Core.Models.Cart
{
    public class ShoppingCartItemViewModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = null!;
        public string BookAuthor { get; set; } = null!;
        public string BookImageUrl { get; set; } = null!;
        public decimal BookPrice { get; set; }
        public string BookDescription { get; set; } = null!;
        public string BookRating { get; set; } = null!;
    }
}
