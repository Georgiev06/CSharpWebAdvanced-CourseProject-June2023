using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Web.Data;
using System.Data;

namespace OnlineShopSystem.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly BookShopDbContext _data;
        private readonly IUserService _userService;

        public UserController(BookShopDbContext data, IUserService userService)
        {
            this._data = data;
            this._userService = userService;
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> All()
        {
            var allUsers = await _userService.GetUsersAsync();

            return View(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string id)
        {
            var user = await _data.Users.FirstOrDefaultAsync(u => u.Id == id);

            var isAdmin = await _data.UserRoles.AnyAsync(ur => ur.UserId == id);

            var adminRole = await _data.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

            if (user != null && !isAdmin)
            {
                user.IsAdmin = true;
                await _data.UserRoles.AddAsync(new IdentityUserRole<string>() { UserId = id, RoleId = adminRole.Id});
                await _data.SaveChangesAsync();
            }

            TempData["Success"] = "User is now admin!";

            return RedirectToAction("All", "User");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _data.Users.FirstOrDefaultAsync(u => u.Id == id);

            var stringGuid = Guid.NewGuid().ToString();

            if (user != null)
            {
                user.FirstName = String.Empty;
                user.LastName = String.Empty;
                user.Address = String.Empty;
                user.Email = String.Empty;
                user.UserName = stringGuid;
                user.PasswordHash = String.Empty;
                user.ShoppingCart = null;
                user.ShoppingCartId = null;
                user.NormalizedEmail = String.Empty;
                user.NormalizedUserName = stringGuid.ToUpper();
                user.SecurityStamp = String.Empty;
                user.ConcurrencyStamp = String.Empty;
                user.IsAdmin = false;

                await _data.SaveChangesAsync();
            }

            TempData["Success"] = "User is deleted!";

            return RedirectToAction("All", "User");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(string id)
        {
            var user = await _data.Users.FirstOrDefaultAsync(u => u.Id == id);

            var isAdmin = await _data.UserRoles.AnyAsync(ur => ur.UserId == id);

            var adminRole = await _data.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

            if (user != null && isAdmin)
            {
                user.IsAdmin = false;
                _data.UserRoles.Remove(new IdentityUserRole<string>() { UserId = id, RoleId = adminRole.Id });
                await _data.SaveChangesAsync();
            }

            TempData["Success"] = "User is no longer admin!";

            return RedirectToAction("All", "User");
        }
    }
}
