using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.User;

namespace Saritasa.BAL.User
{
    public interface IUserService
    {
        Task<string> CreateAsync(RegisterUserRequest request);
        Task<bool> ChangePasswordAsync(ChangePasswordRequest request);
        Task<string> Login(LoginUserRequest request);
    }
}
