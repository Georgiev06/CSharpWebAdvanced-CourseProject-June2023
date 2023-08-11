using OnlineShopSystem.Infrastructure.Data.Models;
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
