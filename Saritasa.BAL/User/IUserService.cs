﻿using System;
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
        Task<string> LoginAsync(LoginUserRequest request);
    }
}
