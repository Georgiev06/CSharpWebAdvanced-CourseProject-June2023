using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Web.Data;

namespace OnlineShopSystem.Tests.SeedData
{
    public static class DatabaseMock
    {
        public static BookShopDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<BookShopDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new BookShopDbContext(dbContextOptions);
            }
        }
    }
}
