using OnlineShopSystem.Core.Models.Cart;
using OnlineShopSystem.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Core.Contracts
{
    public interface IShoppingCartService
    {
        Task AddBookToCartAsync(string userId, int bookId);
        Task RemoveBookFromCartAsync(string userId, int bookId);
        Task ClearCartAsync(string userId);
        ShoppingCart GetCartByUserId(string userId);
    }
}
