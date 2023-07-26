using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Web.Areas.Admin.Models.User;

namespace OnlineShopSystem.Core.Contracts
{
    public interface IUserService
    {
        Task<string> GetFullNameByEmailAsync(string email);

        Task<IEnumerable<UsersViewModel>> GetUsersAsync();
    }
}
