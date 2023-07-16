using OnlineShopSystem.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Core.Models.Cart
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCart> ShoppingCart { get; set; } = null!;
    }
}
