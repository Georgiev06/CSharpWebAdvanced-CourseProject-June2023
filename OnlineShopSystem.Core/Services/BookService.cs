using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Core.Models.Book;
using OnlineShopSystem.Core.Models.Category;
using OnlineShopSystem.Infrastructure.Common;
using OnlineShopSystem.Infrastructure.Data.Models;
using OnlineShopSystem.Web.Data;

namespace OnlineShopSystem.Core.Services
{
    public class BookService : IBookService
    {
        private readonly BookShopDbContext _data;

        public BookService(BookShopDbContext dbContext)
        {
            this._data = dbContext;
        }

        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
        {
            return await _data
                .Books
                .Select(b => new AllBookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price,
                    Rating = b.Rating.ToString(),
                    Category = b.Category.Name
                }).ToListAsync();
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Rating = decimal.Parse(model.Rating),
                CategoryId = model.CategoryId,
            };

            await _data.Books.AddAsync(book);
            await _data.SaveChangesAsync();
        }

        public async Task<AddBookViewModel> GetAddBookModelAsync()
        {
            var categories = await _data.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();

            var model = new AddBookViewModel()
            {
                Categories = categories
            };

            return model;
        }

        public async Task EditBookAsync(EditBookViewModel model, int id)
        {
            var book = await _data.Books.FindAsync(id);

            if (book != null)
            {
                book.Title = model.Title;
                book.Author = model.Author;
                book.Description = model.Description;
                book.ImageUrl = model.ImageUrl;
                book.Price = model.Price;
                book.Rating = decimal.Parse(model.Rating);
                book.CategoryId = model.CategoryId;

                await _data.SaveChangesAsync();
            }
        }

        public async Task<EditBookViewModel?> GetBookByIdForEditAsync(int id)
        {
            var categories = await _data.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();

            return await _data.Books
                .Where(b => b.Id == id)
                .Select(b => new EditBookViewModel()
                {
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price,
                    Rating = b.Rating.ToString(),
                    CategoryId = b.CategoryId,
                    Categories = categories
                }).FirstOrDefaultAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _data.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book != null)
            {
                _data.Books.Remove(book);
                await _data.SaveChangesAsync();
            }
        }

        public async Task AddBookToFavoritesAsync(string userId, BookViewModel book)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveBookFromFavoritesAsync(string userId, BookViewModel book)
        {
            throw new NotImplementedException();
        }
    }
}
