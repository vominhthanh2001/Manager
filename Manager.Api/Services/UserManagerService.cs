using Manager.Api.Data;
using Manager.Shared.Contracts;
using Manager.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.Marshalling;

namespace Manager.Api.Services
{
    public class UserManagerService : IUserManager
    {
        private DbSet<UserModel> _users => _context.Users;
        private readonly ServerDbContext _context;
        private readonly IListUserManager _service;
        private readonly IProductManager _serviceProduct;
        private readonly TokenService _tokenService;

        public UserManagerService(ServerDbContext context, IListUserManager service, IProductManager serverProduct, TokenService tokenService)
        {
            _context = context;
            _service = service;
            _serviceProduct = serverProduct;
            _tokenService = tokenService;
        }

        public Task<UserModel> ForgotPassword(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseModel> Login(UserModel? user)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                var userFind = _users.ToList().FirstOrDefault(x => x.License == user.License);
                if (userFind is null)
                    throw new ArgumentNullException(nameof(userFind));

                bool isCompare = userFind.CompareDateTime();
                if (!isCompare)
                {
                    return new UserResponseModel
                    {
                        Status = false,
                        Message = "Hết hạn sử dụng",
                        Token = string.Empty
                    };
                }

                string tokenAuthentication = _tokenService.GenerateToken(userFind);

                #region Update Authentication
                if (userFind.Authentication is null)
                {
                    userFind.Authentication = new AuthenticationModel();
                    userFind.Authentication.Jwt = tokenAuthentication;

                    await _context.SaveChangesAsync();
                }
                #endregion

                return new UserResponseModel
                {
                    Status = true,
                    Message = "Đăng nhập thành công",
                    Token = tokenAuthentication
                };
            }
            catch (Exception ex) { }

            return new UserResponseModel
            {
                Status = false,
                Message = "Đăng nhập thất bại",
                Token = string.Empty
            };
        }

        public Task<UserResponseModel> Logout(UserModel? user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseModel> Register(UserModel? user)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                var userFind = _users.ToList().FirstOrDefault(x => x.License == user.License);
                if (userFind is not null)
                {
                    return new UserResponseModel
                    {
                        Status = false,
                        Message = "Tạo tài khoản tồn tại",
                        Token = string.Empty
                    };
                }

                #region Fix Product
                string nameProduct = user.Product.Name;
                var productFind = await _serviceProduct.FindAsync(nameProduct);
                if (productFind is not null)
                    user.Product = productFind;
                #endregion

                var resAddAsync = await _service.AddAsync(user);
                if (!resAddAsync.Status)
                    throw new Exception();

                return new UserResponseModel
                {
                    Status = true,
                    Message = "Tạo tài khoản thành công",
                    Token = string.Empty
                };
            }
            catch (Exception ex) { }

            return new UserResponseModel
            {
                Status = false,
                Message = "Tào tài khoản thất bại",
                Token = string.Empty
            };
        }
    }
}
