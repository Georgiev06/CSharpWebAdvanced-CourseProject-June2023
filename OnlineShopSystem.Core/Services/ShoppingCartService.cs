using Microsoft.EntityFrameworkCore;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Core.Models.Cart;
using OnlineShopSystem.Infrastructure.Data.Models;
using OnlineShopSystem.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Core.Models.Book;

namespace OnlineShopSystem.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly BookShopDbContext _data;

        public ShoppingCartService(BookShopDbContext dbContext)
        {
            _data = dbContext;
        }

        public async Task AddBookToCartAsync(string userId, int bookId, int quantity)
        {
            var cart = GetCartByUserId(userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    Books = new List<Book>()
                };

                 await _data.ShoppingCart.AddAsync(cart);
            }

            var book = await _data.Books.FindAsync(bookId);

            if (book == null)
            {
                // Handle book not found error
                //return;
            }

            if (cart.Books.Any(b => b.Id == bookId))
            {
                // Increase the quantity if the book is already in the cart
                var cartItem = cart.Books.First(b => b.Id == bookId);

            }
            else
            {
                // Add the book to the cart with the specified quantity
                if (book != null)
                {
                    cart.Books.Add(new Book
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Price = book.Price,
                    });
                }
            }

             await _data.SaveChangesAsync();
        }

        public async Task RemoveBookFromCartAsync(string userId, int bookId)
        {
            var cart = GetCartByUserId(userId);

            if (cart == null)
            {
                // Handle cart not found error
                //return;
            }

            var book = cart.Books.FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                 cart.Books.Remove(book);
                 await _data.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantityAsync(int productId, string userId, int quantity)
        {
            //var cart = GetCartByUserId(userId);

            //if (cart == null)
            //{
            //    // Handle cart not found error
            //    //return;
            //}

            //var book = cart.Books.FirstOrDefault(b => b.Id == productId);

            //if (book != null)
            //{
            //    book.Quantity = quantity; ->> you should update your book entity to have quantity property in order UpdateQuantityAsync to work
            //    await _data.SaveChangesAsync();
            //}
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
            return _data.ShoppingCart
                .FirstOrDefault(c => c.UserId == userId);
        }
    }
}
