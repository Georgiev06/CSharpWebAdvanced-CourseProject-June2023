﻿using OnlineShopSystem.Web.Areas.Admin.Models.User;

namespace OnlineShopSystem.Core.Contracts
{
    public interface IUserService
    {
        Task<string> GetFullNameByEmailAsync(string email);

        Task<IEnumerable<UsersViewModel>> GetUsersAsync();
    }
}
