using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OnlineShopSystem.Core.Models.Book;
using OnlineShopSystem.Core.Services;
using OnlineShopSystem.Infrastructure.Data.Models;
using OnlineShopSystem.Tests.SeedData;

namespace OnlineShopSystem.Tests.UnitTests
{
    [TestFixture]
    public class ShoppingCartServiceTests
    {
        [Test]
        public async Task AddBookToCartAsync_WithValidData_ShouldAddBookToCart()
        {
            // Arrange
            var dbContext = DatabaseMock.Instance;
            var shoppingCartService = new ShoppingCartService(dbContext);

            var user = new User()
            {
                Id = "0e5e63f5-8ce3-4815-bc73-189fb1a33aee",
                FirstName = "John",
                LastName = "Doe",
                Address = "Sofia, Bulgaria"
            };

            var book = new Book()
            {
                Id = 1,
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            // Act
            await shoppingCartService.AddBookToCartAsync(user.Id, book.Id);

            // Assert
            var shoppingCart = dbContext.ShoppingCarts.FirstOrDefault(sc => sc.UserId == user.Id);
            Assert.That(shoppingCart, Is.Not.Null);
            Assert.That(shoppingCart.Books.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AddBookToCartAsync_WithInvalidData_ShouldNotAddBookToCart()
        {
            var dbContext = DatabaseMock.Instance;
            var shoppingCartService = new ShoppingCartService(dbContext);

            var user = new User()
            {
                Id = "0e5e63f5-8ce3-4815-bc73-189fb1a33aee",
                FirstName = "John",
                LastName = "Doe",
                Address = "Sofia, Bulgaria"
            };

            var book = new Book()
            {
                Id = 1,
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            // Act
            await shoppingCartService.AddBookToCartAsync(user.Id, 2);
            await dbContext.SaveChangesAsync();

            // Assert
            var shoppingCart = dbContext.ShoppingCarts.FirstOrDefault(sc => sc.UserId == user.Id);
            Assert.That(shoppingCart.Books.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task RemoveBookFromCartAsync_WithValidData_ShouldRemoveBookFromCart()
        {
            var dbContext = DatabaseMock.Instance;
            var shoppingCartService = new ShoppingCartService(dbContext);

            var user = new User()
            {
                Id = "0e5e63f5-8ce3-4815-bc73-189fb1a33aee",
                FirstName = "John",
                LastName = "Doe",
                Address = "Sofia, Bulgaria"
            };

            var book = new Book()
            {
                Id = 1,
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            // Act
            await shoppingCartService.AddBookToCartAsync(user.Id, book.Id);
            await shoppingCartService.RemoveBookFromCartAsync(user.Id, book.Id);
            await dbContext.SaveChangesAsync();

            // Assert
            var shoppingCart = dbContext.ShoppingCarts.FirstOrDefault(sc => sc.UserId == user.Id);
            Assert.That(shoppingCart.Books.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task RemoveBookFromCartAsync_WithInvalidData_ShouldNotRemoveBookFromCart()
        {
            var dbContext = DatabaseMock.Instance;
            var shoppingCartService = new ShoppingCartService(dbContext);

            var user = new User()
            {
                Id = "0e5e63f5-8ce3-4815-bc73-189fb1a33aee",
                FirstName = "John",
                LastName = "Doe",
                Address = "Sofia, Bulgaria"
            };

            var book = new Book()
            {
                Id = 1,
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            // Act
            await shoppingCartService.AddBookToCartAsync(user.Id, book.Id);
            await shoppingCartService.RemoveBookFromCartAsync(user.Id, 2);
            await dbContext.SaveChangesAsync();

            // Assert
            var shoppingCart = dbContext.ShoppingCarts.FirstOrDefault(sc => sc.UserId == user.Id);
            Assert.That(shoppingCart.Books.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task ClearCartAsync_WithValidData_ShouldRemoveAllBooksFromCart()
        {
            var dbContext = DatabaseMock.Instance;
            var shoppingCartService = new ShoppingCartService(dbContext);

            var user = new User()
            {
                Id = "0e5e63f5-8ce3-4815-bc73-189fb1a33aee",
                FirstName = "John",
                LastName = "Doe",
                Address = "Sofia, Bulgaria"
            };

            var book = new Book()
            {
                Id = 1,
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            };

            var book2 = new Book()
            {
                Id = 2,
                Title = "Dark Force Rising: Star Wars Legends",
                Author = "Timothy Zahn",
                Description =
                    "The dying Empire’s most cunning and ruthless warlord, Grand Admiral Thrawn, has taken command of the remnants of the Imperial Fleet and launched a massive campaign aimed at the New Republic’s destruction. Meanwhile, Han Solo and Lando Calrissian race against time to find proof of treason inside the highest Republic Council—only to discover instead a ghostly fleet of warships that could bring doom to their friends and victory to their enemies.\r\nYet most dangerous of all is a new Dark Jedi, risen from the ashes of a shrouded past, consumed by bitterness, and scheming to corrupt Luke Skywalker to the dark side.",
                Price = (decimal)5.49,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/51SmuA28T+S._SX330_BO1,204,203,200_.jpg",
                Rating = (decimal)8.99
            };

            var books = new List<Book>() { book, book2 };

            await dbContext.Users.AddAsync(user);
            await dbContext.Books.AddRangeAsync(books);
            await dbContext.SaveChangesAsync();

            // Act
            await shoppingCartService.AddBookToCartAsync(user.Id, book.Id);
            await shoppingCartService.AddBookToCartAsync(user.Id, book2.Id);
            await shoppingCartService.ClearCartAsync(user.Id);
            await dbContext.SaveChangesAsync();

            // Assert
            var shoppingCart = dbContext.ShoppingCarts.FirstOrDefault(sc => sc.UserId == user.Id);
            Assert.That(shoppingCart.Books.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task ClearCartAsync_WithInvalidData_ShouldNotRemoveAllBooksFromCart()
        {
            var dbContext = DatabaseMock.Instance;
            var shoppingCartService = new ShoppingCartService(dbContext);

            var user = new User()
            {
                Id = "0e5e63f5-8ce3-4815-bc73-189fb1a33aee",
                FirstName = "John",
                LastName = "Doe",
                Address = "Sofia, Bulgaria"
            };

            var book = new Book()
            {
                Id = 1,
                Title = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                Description =
                    "Harry Potter spent ten long years living with Mr. and Mrs. Dursley, an aunt and uncle whose outrageous favoritism of their perfectly awful son Dudley leads to some of the most inspired dark comedy since Charlie and the Chocolate Factory. But fortunately for Harry, he's about to be granted a scholarship to a unique boarding school called THE HOGWORTS SCHOOL OF WITCHCRAFT AND WIZARDRY, where he will become a school hero at the game of Quidditch (a kind of aerial soccer played high above the ground on broomsticks), he will make some wonderful friends, and, unfortunately, a few terrible enemies. For although he seems to be getting your run-of-the-mill boarding school experience (well, ok, even that's pretty darn out of the ordinary), Harry Potter has a destiny that he was born to fulfill. A destiny that others would kill to keep him from.",
                Price = (decimal)11.35,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/515E2f9WO+L.jpg",
                Rating = (decimal)7.98
            };

            var book2 = new Book()
            {
                Id = 2,
                Title = "Dark Force Rising: Star Wars Legends",
                Author = "Timothy Zahn",
                Description =
                    "The dying Empire’s most cunning and ruthless warlord, Grand Admiral Thrawn, has taken command of the remnants of the Imperial Fleet and launched a massive campaign aimed at the New Republic’s destruction. Meanwhile, Han Solo and Lando Calrissian race against time to find proof of treason inside the highest Republic Council—only to discover instead a ghostly fleet of warships that could bring doom to their friends and victory to their enemies.\r\nYet most dangerous of all is a new Dark Jedi, risen from the ashes of a shrouded past, consumed by bitterness, and scheming to corrupt Luke Skywalker to the dark side.",
                Price = (decimal)5.49,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/51SmuA28T+S._SX330_BO1,204,203,200_.jpg",
                Rating = (decimal)8.99
            };

            var books = new List<Book>() { book, book2 };

            await dbContext.Users.AddAsync(user);
            await dbContext.Books.AddRangeAsync(books);
            await dbContext.SaveChangesAsync();

            // Act
            await shoppingCartService.AddBookToCartAsync(user.Id, book.Id);
            await shoppingCartService.AddBookToCartAsync(user.Id, book2.Id);
            await shoppingCartService.ClearCartAsync("invalidId");

            await dbContext.SaveChangesAsync();

            // Assert
            var shoppingCart = dbContext.ShoppingCarts.FirstOrDefault(sc => sc.UserId == user.Id);
            Assert.That(shoppingCart.Books.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetCartByUserId_WithValidData_ShouldReturnCartId()
        {
            //Arrange
            var dbContext = DatabaseMock.Instance;
            var shoppingCartService = new ShoppingCartService(dbContext);

            var user = new User()
            {
                Id = "0e5e63f5-8ce3-4815-bc73-189fb1a33aee",
                FirstName = "John",
                LastName = "Doe",
                Address = "Sofia, Bulgaria"
            };

            var shoppingCart = new ShoppingCart()
            {
                Id = 1,
                UserId = user.Id
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.ShoppingCarts.AddAsync(shoppingCart);
            await dbContext.SaveChangesAsync();

            // Act
            var result = shoppingCartService.GetCartByUserId(user.Id);

            // Assert
            Assert.AreEqual(result.Id, shoppingCart.Id);
            Assert.AreEqual(result.User.Id, shoppingCart.User.Id);
        }

    }
}
