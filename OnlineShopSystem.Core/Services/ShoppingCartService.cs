using Microsoft.EntityFrameworkCore;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Infrastructure.Data.Models;
using OnlineShopSystem.Web.Data;
using Book = OnlineShopSystem.Infrastructure.Data.Models.Book;

namespace OnlineShopSystem.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly BookShopDbContext _data;

        public ShoppingCartService(BookShopDbContext dbContext)
        {
            this._data = dbContext;
        }

        public async Task AddBookToCartAsync(string userId, int bookId)
        {
            var cart = GetCartByUserId(userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    Books = new List<Book>()
                };

                await _data.ShoppingCarts.AddAsync(cart);
            }

            var book = await _data.Books.FindAsync(bookId);

            if (book == null)
            {
                return;
            }

            if (!cart.Books.Any(b => b.Id == bookId))
            {
                cart.Books.Add(book);
            }

            await _data.SaveChangesAsync();
        }

        public async Task RemoveBookFromCartAsync(string userId, int bookId)
        {
            var cart = GetCartByUserId(userId);

            if (cart == null)
            {
                return;
            }

            var book = cart.Books.FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                 cart.Books.Remove(book);
                 await _data.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null)
            {
                cart.Books.Clear();
                await _data.SaveChangesAsync();
            }
        }

        public ShoppingCart GetCartByUserId(string userId)
        {
            return _data.ShoppingCarts
                .Include(b => b.Books)
                .FirstOrDefault(c => c.UserId == userId);
        }
    }
}
