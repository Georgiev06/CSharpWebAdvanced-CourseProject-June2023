using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineShopSystem.Core.Services;
using OnlineShopSystem.Infrastructure.Data.Models;
using OnlineShopSystem.Tests.SeedData;
using OnlineShopSystem.Web.Data;

namespace OnlineShopSystem.Tests.UnitTests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        [Test]
        public async Task GetAllCategoryNamesAsync_WithCategories_ShouldReturnAllCategoryNames()
        {
            var dbContext = DatabaseMock.Instance;
            var categoryService = new CategoryService(dbContext);

            var categories = new List<Category>
            {
                new Category { Name = "Fantasy" },
                new Category { Name = "Romance" },
                new Category { Name = "Westerns" },
            };

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();

            var expected = categories.Select(c => c.Name).ToArray();

            var actual = await categoryService.GetAllCategoryNamesAsync();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetAllCategoryNamesAsync_WithoutCategories_ShouldReturnEmptyCollection()
        {
            var dbContext = DatabaseMock.Instance;
            var categoryService = new CategoryService(dbContext);

            var expected = new string[0];

            var actual = await categoryService.GetAllCategoryNamesAsync();

            Assert.AreEqual(expected, actual);
        }
    }
}
