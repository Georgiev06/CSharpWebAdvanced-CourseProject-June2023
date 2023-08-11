using Microsoft.EntityFrameworkCore;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Core.Models.Book;
using OnlineShopSystem.Core.Models.Book.Enum;
using OnlineShopSystem.Core.Models.Category;
using OnlineShopSystem.Core.Models.Review;
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

        public async Task<AllBooksFilteredAndPagedServiceModel> GetAllBooksAsync(AllBooksQueryModel queryModel, string userId)
        {
            IQueryable<Book> booksQuery = this._data.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                booksQuery = booksQuery.Where(b => b.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                booksQuery = booksQuery.Where(b => 
                    b.Title.ToLower().Contains(queryModel.SearchTerm.ToLower()) || 
                    b.Author.ToLower().Contains(queryModel.SearchTerm.ToLower()) || 
                    b.Description.ToLower().Contains(queryModel.SearchTerm.ToLower()));
            }

            if (queryModel.Sorting != null)
            {
                switch (queryModel.Sorting)
                {
                    case BookSorting.PriceAscending:
                        booksQuery = booksQuery.OrderBy(b => b.Price);
                        break;
                    case BookSorting.PriceDescending:
                        booksQuery = booksQuery.OrderByDescending(b => b.Price);
                        break;
                    case BookSorting.RatingAscending:
                        booksQuery = booksQuery.OrderBy(b => b.Rating);
                        break;
                    case BookSorting.RatingDescending:
                        booksQuery = booksQuery.OrderByDescending(b => b.Rating);
                        break;
                    default:
                        break;
                }
            }

            var books = await booksQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.BooksPerPage)
                .Take(queryModel.BooksPerPage)
                .Select(b => new AllBookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price,
                    Rating = b.Rating.ToString(),
                    Category = b.Category.Name,
                    UserId = userId
                })
                .ToListAsync();

            var totalBooks = booksQuery.Count();

            return new AllBooksFilteredAndPagedServiceModel
            {
                Books = books,
                TotalBooksCount = totalBooks
            };
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
                Rating = 0M,
                CategoryId = model.CategoryId,
            };


            await _data.Books.AddAsync(book);
            await _data.SaveChangesAsync();

            //var bookById = await GetBookByIdAsync(book.Id);
            //await AddBookToFavoritesAsync(userId, bookById);
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
                book.Rating = model.Rating;
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
                    Rating = b.Rating,
                    CategoryId = b.CategoryId,
                    Categories = categories
                }).FirstOrDefaultAsync();
        }

        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {
            return await _data.Books
                .Where(b => b.Id == id)
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description,
                    Rating = b.Rating,
                    CategoryId = b.CategoryId
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

        public async Task<DetailsBookViewModel> BookDetailsByIdAsync(int id)
        {
            return await _data.Books
                .Where(b => b.Id == id)
                .Select(b => new DetailsBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Price = b.Price,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description,
                    Rating = b.Rating.ToString(),
                    Category = b.Category.Name
                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId)
        {
            return await _data.UsersBooks
                .Where(ub => ub.UserId == userId)
                .Select(b => new AllBookViewModel
                {
                    Id = b.Book.Id,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    Description = b.Book.Description,
                    ImageUrl = b.Book.ImageUrl,
                    Rating = b.Book.Rating.ToString(),
                    Price = b.Book.Price,
                    Category = b.Book.Category.Name
                }).ToListAsync();
        }

        public async Task AddBookToFavoritesAsync(string userId, BookViewModel book)
        {
            bool IsAdded = await _data.UsersBooks.AnyAsync(ub => ub.UserId == userId && ub.BookId == book.Id);

            if (IsAdded == false)
            {
                var userBook = new UserBook
                {
                    UserId = userId,
                    BookId = book.Id,
                };

                await _data.UsersBooks.AddAsync(userBook);
                await _data.SaveChangesAsync();
            }
        }

        public async Task RemoveBookFromFavoritesAsync(string userId, BookViewModel book)
        {
            var userBook = await _data.UsersBooks
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == book.Id);

            if (userBook != null)
            {
                _data.UsersBooks.Remove(userBook);
                await _data.SaveChangesAsync();
            }
        }

        public async Task AddReviewAsync(AddReviewViewModel review)
        {
            var reviewToAdd = new Review
            {
                UserId = review.UserId,
                BookId = review.BookId,
                Comment = review.Comment,
                Rating = review.Rating
            };

            await _data.Reviews.AddAsync(reviewToAdd);
            await _data.SaveChangesAsync();

            var book = await _data.Books.FirstOrDefaultAsync(b => b.Id == review.BookId);

            book!.Rating = GetAverageRating(review.BookId);
            await _data.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<ReviewViewModel>> GetReviewAsync(int bookId)
        {
            return await _data.Reviews
                .Where(r => r.BookId == bookId)
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    BookId = r.BookId,
                    Content = r.Comment,
                    Rating = r.Rating,
                    UserUsername = r.User.FirstName + " " + r.User.LastName
                }).ToListAsync();
        }

        private decimal GetAverageRating(int bookId)
        {
            if (_data.Reviews.Count() == null || _data.Reviews.Count() == 0)
            {
                return 0;
            }

            var averageRating = _data.Reviews
                .Where(r => r.BookId == bookId)
                .Average(r => r.Rating);

            return (decimal)averageRating;
        }
    }
}
