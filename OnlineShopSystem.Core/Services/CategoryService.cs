using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Web.Data;

namespace OnlineShopSystem.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BookShopDbContext _data;
        public CategoryService(BookShopDbContext dbContext)
        {
            this._data = dbContext;
        }
        public async Task<IEnumerable<string>> GetAllCategoryNamesAsync()
        {
            var allNames = await _data
                .Categories
                .Select(c => c.Name)
                .ToArrayAsync();

            return allNames;
        }
    }
}
