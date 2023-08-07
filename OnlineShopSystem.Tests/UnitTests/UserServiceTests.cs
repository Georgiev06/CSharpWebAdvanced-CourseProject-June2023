using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Core.Services;
using OnlineShopSystem.Infrastructure.Data.Models;
using OnlineShopSystem.Tests.SeedData;
using OnlineShopSystem.Web.Areas.Admin.Models.User;

namespace OnlineShopSystem.Tests.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public async Task GetFullNameByEmailAsync_WithUser_ShouldReturnFullName()
        {
            var dbContext = DatabaseMock.Instance;
            var userService = new UserService(dbContext);

            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Address = "Sofia",
            };

            await dbContext.Users.AddAsync(user);

            await dbContext.SaveChangesAsync();

            var expected = $"{user.FirstName} {user.LastName}";

            var actual = await userService.GetFullNameByEmailAsync(user.Email);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetFullNameByEmailAsync_WithoutUser_ShouldReturnEmptyString()
        {
            var dbContext = DatabaseMock.Instance;
            var userService = new UserService(dbContext);

            var expected = string.Empty;

            var actual = await userService.GetFullNameByEmailAsync("");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetUsersAsync_WithUsers_ShouldReturnAllUsers()
        {
            var dbContext = DatabaseMock.Instance;
            var userService = new UserService(dbContext);

            var users = new List<User>
            {
                new User
                {
                    //Id = "1",
                    //Email = "john@gmail.com",
                    FirstName = "John",
                    LastName = "Doe",
                    Address = "Sofia",
                    //IsAdmin = true
                },

                new User
                {
                    //Id = "2",
                    //Email = "jane@gmail.com",
                    FirstName = "Jane",
                    LastName = "Doe",
                    Address = "Sofia",
                    //IsAdmin = false
                },

            };

            await dbContext.Users.AddRangeAsync(users);

            await dbContext.SaveChangesAsync();

            var expected = users.Select(u => new UsersViewModel()
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsAdmin = u.IsAdmin
            }).ToList();

            var actual = (await userService.GetUsersAsync()).ToList();

            Assert.AreEqual(expected.Count, actual.Count);
        }

        [Test]

        public async Task GetUsersAsync_WithoutUsers_ShouldReturnEmptyList()
        {
            var dbContext = DatabaseMock.Instance;
            var userService = new UserService(dbContext);

            var expected = new List<UsersViewModel>();

            var actual = (await userService.GetUsersAsync()).ToList();

            Assert.AreEqual(expected.Count, actual.Count);
        }
    }
}
