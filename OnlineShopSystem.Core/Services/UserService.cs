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
    public class UserService : IUserService
    {
        private readonly BookShopDbContext _data;

        public UserService(BookShopDbContext dbContext)
        {
            this._data = dbContext;
        }

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            var user = await _data.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }
    }
}
