using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Core.Contracts
{
    public interface IUserService
    {
        Task<string> GetFullNameByEmailAsync(string email);
    }
}
