using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saritasa.Data.Entities;

namespace Saritasa.BAL.User
{
    public interface IUserManager
    {
        Task<IdentityResult> RegisterAsync(Data.Entities.User user, string password);
        Task<Data.Entities.User> FindByIdAsync(string userId);
        Task<IdentityResult> ChangePasswordAsync(Data.Entities.User user, string currentPassword, string newPassword);
    }
}
