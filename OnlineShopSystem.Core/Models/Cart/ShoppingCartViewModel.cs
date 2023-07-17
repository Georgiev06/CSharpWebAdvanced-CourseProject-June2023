using OnlineShopSystem.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Core.Models.Book;

namespace OnlineShopSystem.Core.Models.Cart
{
    public class ShoppingCartViewModel
    {
        public string UserId { get; set; } = null!;
        public List<ShoppingCartItemViewModel> Items { get; set; } = null!;
    }
}
