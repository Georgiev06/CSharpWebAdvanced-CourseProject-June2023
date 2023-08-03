using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Language.Flow;
using NUnit.Framework.Internal;
using OnlineShopSystem.Core.Models.Book;
using OnlineShopSystem.Core.Models.Book.Enum;
using OnlineShopSystem.Core.Models.Category;
using OnlineShopSystem.Core.Services;
using OnlineShopSystem.Infrastructure.Data.Models;
using OnlineShopSystem.Tests.SeedData;
using OnlineShopSystem.Web.Data;

namespace OnlineShopSystem.Tests.UnitTests
{
    [TestFixture]
    public class BookServiceTests
    {
        [Test]
        public async Task AddBookAsync_WithValidData_ShouldAddBookCorrectly()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            var user = new User()
            {
                Id = "0e5e63f5-8ce3-4815-bc73-189fb1a33aee",
            };

            var book = new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            };

            await bookService.AddBookAsync(user.Id, book);

            Assert.That(dbContext.Books.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task AddBookAsync_WithInvalidData_ShouldThrowException()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            var user = new User()
            {
                Id = "0e5e63f5-8ce3-4815-bc73-189fb1a33aee",
            };

            var book = new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            };

            await bookService.AddBookAsync(user.Id, book);

            Assert.That(dbContext.Books.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAddBookModelAsync_WithValidData_ShouldReturnValidCategory()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            var category = new Category()
            {
                Id = 1,
                Name = "Fantasy"
            };

            dbContext.Categories.Add(category);

            await dbContext.SaveChangesAsync();

            var result = await bookService.GetAddBookModelAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Categories, Is.Not.Null);
            Assert.That(result.Categories.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAddBookModelAsync_WithInvalidData_ShouldReturnEmptyCategory()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            var result = await bookService.GetAddBookModelAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Categories, Is.Not.Null);
            Assert.That(result.Categories.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task EditBookAsync_WithValidData_ShouldEditBookCorrectly()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });

            await dbContext.SaveChangesAsync();

            var book = new EditBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            };

            await bookService.EditBookAsync(book, 1);

            var result = dbContext.Books.FirstOrDefault();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(book.Title));
            Assert.That(result.Author, Is.EqualTo(book.Author));
            Assert.That(result.Description, Is.EqualTo(book.Description));
            Assert.That(result.Price, Is.EqualTo(book.Price));
            Assert.That(result.CategoryId, Is.EqualTo(book.CategoryId));
            Assert.That(result.ImageUrl, Is.EqualTo(book.ImageUrl));
            Assert.That(result.Rating, Is.EqualTo(book.Rating));
        }

        [Test]
        public async Task EditBookAsync_WithInvalidData_ShouldNotWork()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            var book = new EditBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            };

            await bookService.EditBookAsync(book, 1);

            var result = dbContext.Books.FirstOrDefault();

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetBookByIdForEditAsync_WithValidData_ShouldReturnBookCorrectly()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });


            var result = await bookService.GetBookByIdForEditAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("Harry Potter and the Sorcerer's Stone"));
            Assert.That(result.Author, Is.EqualTo("J.K. Rowling"));
            Assert.That(result.Description, Is.EqualTo(
                               "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from."));
            Assert.That(result.Price, Is.EqualTo((decimal)11.35));
            Assert.That(result.CategoryId, Is.EqualTo(1));
            Assert.That(result.ImageUrl, Is.EqualTo("https://m.media-amazon.com/images/I/515E2f9WO+L.jpg"));
            Assert.That(result.Rating, Is.EqualTo((decimal)7.98));
        }

        [Test]
        public async Task GetBookByIdForEditAsync_WithInvalidData_ShouldReturnNull()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            var result = await bookService.GetBookByIdForEditAsync(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetBookByIdAsync_WithValidData_ShouldReturnBook()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });

            await bookService.GetBookByIdAsync(1);

            var result = dbContext.Books.FirstOrDefault();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetBookByIdAsync_WithInvalidData_ShouldReturnNull()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            var result = await bookService.GetBookByIdAsync(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetMyBooksAsync_WithValidData_ShouldReturnBookInUserCollection()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });

            var result = await bookService.GetMyBooksAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee");

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetMyBooksAsync_WithInvalidData_ShouldReturnEmpty()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            var result = await bookService.GetMyBooksAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee");

            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task DeleteBookAsync_WithValidData_ShouldDeleteBookCorrectly()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });

            await dbContext.SaveChangesAsync();

            await bookService.DeleteBookAsync(1);

            var result = dbContext.Books.FirstOrDefault();

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteBookAsync_WithInvalidData_ShouldNotWork()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });

            await dbContext.SaveChangesAsync();

            await bookService.DeleteBookAsync(2);

            var result = dbContext.Books.FirstOrDefault();

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task BookDetailsByIdAsync_WithValidData_ShouldReturnBook()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });

            await dbContext.SaveChangesAsync();

            await bookService.BookDetailsByIdAsync(1);

            var result = dbContext.Books.FirstOrDefault(b => b.Id == 1);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task BookDetailsByIdAsync_WithInvalidData_ShouldReturnNull()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.BookDetailsByIdAsync(1);

            var result = dbContext.Books.FirstOrDefault(b => b.Id == 1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddBookToFavoritesAsync_WithValidData_ShouldWorkCorrectly()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });

            await dbContext.SaveChangesAsync();

            var currentBook = await bookService.GetBookByIdAsync(1);

            await bookService.AddBookToFavoritesAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", currentBook);

            Assert.That(dbContext.UsersBooks.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveBookFromFavoritesAsync_WithValidData_ShouldWorkCorrectly()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });

            await dbContext.SaveChangesAsync();

            var currentBook = await bookService.GetBookByIdAsync(1);

            await bookService.AddBookToFavoritesAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", currentBook);

            await bookService.RemoveBookFromFavoritesAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", currentBook);

            Assert.That(dbContext.UsersBooks.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllBooksAsync_WithValidData_ShouldReturnBookByGivenCriteria()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });

            await dbContext.SaveChangesAsync();

            var result = await bookService.GetAllBooksAsync(new AllBooksQueryModel()
            {
                SearchTerm = "Harry Potter",
            }, "0e5e63f5-8ce3-4815-bc73-189fb1a33aee");

            Assert.That(result.TotalBooksCount, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllBooksAsync_WithValidData_ShouldReturnAllBooks()
        {
            var dbContext = DatabaseMock.Instance;
            var bookService = new BookService(dbContext);

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            });

            await bookService.AddBookAsync("0e5e63f5-8ce3-4815-bc73-189fb1a33aee", new AddBookViewModel()
            {
                Title = "No Country for Old Men",
                Author = "Cormac McCarthy",
                Description =
                    "The time is our own, when rustlers have given way to drug-runners and small towns have become free-fire zones. One day, a good old boy named Llewellyn Moss finds a pickup truck surrounded by a bodyguard of dead men. A load of heroin and two million dollars in cash are still in the back. When Moss takes the money, he sets off a chain reaction of catastrophic violence that not even the law—in the person of aging, disillusioned Sheriff Bell—can contain.\r\nAs Moss tries to evade his pursuers—in particular a mysterious mastermind who flips coins for human lives—McCarthy simultaneously strips down the American crime novel and broadens its concerns to encompass themes as ancient as the Bible and as bloodily contemporary as this morning’s headlines.\r\nNo Country for Old Men is a triumph.",
                Price = (decimal)12.50,
                CategoryId = 3,
                ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRDj_GKFvKOaRdINjAU8eC4FIfMSw8S43oZdwUT9AAlDog3Dm5S",
                Rating = (decimal)10.00
            });

            await dbContext.SaveChangesAsync();

            var result = await bookService.GetAllBooksAsync(new AllBooksQueryModel()
            {
                SearchTerm = "",
            }, "0e5e63f5-8ce3-4815-bc73-189fb1a33aee");

            Assert.That(result.TotalBooksCount, Is.EqualTo(2));
        }
    }
}