using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saritasa.BAL.User
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<Data.Entities.User> _userManager;
        public UserManager(UserManager<Data.Entities.User> userManager)
        {
            _userManager = userManager;
        }
        public Task<IdentityResult> ChangePasswordAsync(Data.Entities.User user, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public Task<Data.Entities.User> FindByIdAsync(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<IdentityResult> RegisterAsync(Data.Entities.User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }
    }
}
