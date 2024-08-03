using Manager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Contracts
{
    public interface IUserManager
    {
        Task<UserResponseModel> Login(UserModel? user);
        Task<UserResponseModel> Logout(UserModel? user);
        Task<UserResponseModel> Register(UserModel? user);
        Task<UserModel> ForgotPassword(string username);
    }
}
