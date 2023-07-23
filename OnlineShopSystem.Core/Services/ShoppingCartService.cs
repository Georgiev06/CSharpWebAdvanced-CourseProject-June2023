﻿using Microsoft.EntityFrameworkCore;
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
using static OnlineShopSystem.Infrastructure.Common.EntityValidationConstants;
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

                await _data.ShoppingCart.AddAsync(cart);
            }

            var book = await _data.Books.FindAsync(bookId);

            if (book == null)
            {
                // Handle book not found error
                // return;
            }

            if (!cart.Books.Any(b => b.Id == bookId))
            {
                // Add the book to the cart if it is not already present
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
            return _data.ShoppingCart
                .Include(b => b.Books)
                .FirstOrDefault(c => c.UserId == userId);
        }
    }
}