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

                var allUsers = await _context.IncludeGetAllUsers();
                var userFind = allUsers.FirstOrDefault(x => x.License == user.License);
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

                #region Update And Check Online & Max Device
                if (userFind.Device is null)
                    userFind.Device = new DeviceModel();

                if (userFind.Device.IsOnline && userFind.Device.MaxDevice == userFind.Device.TotalOnline)
                {
                    return new UserResponseModel
                    {
                        Status = false,
                        Message = "Tài khoản này đã được đăng nhập ở thiết bị khác",
                        Token = string.Empty
                    };
                }
                userFind.Device.ActionOnline();
                #endregion

                string tokenAuthentication = _tokenService.GenerateToken(userFind);

                #region Update Authentication
                if (userFind.Authentication is null)
                {
                    userFind.Authentication = new AuthenticationModel();
                }

                userFind.Authentication.Jwt = tokenAuthentication;
                #endregion

                await _context.SaveChangesAsync();

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

        public async Task<UserResponseModel> Logout(UserModel? user)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                int id = user.Id;
                var users = await _context.IncludeGetAllUsers();
                var userFind = users.FirstOrDefault(x => x.License == user.License);
                if (userFind is null)
                    throw new Exception();

                /*
                 * khi tất cả các thiết bị logout hết thì tiến hành xóa mã authentication
                 * tính cho trường hợp nếu bị lộ mã jwt
                 */
                bool isEmptyToken = userFind.Device.ActionOffline();
                if (isEmptyToken)
                {
                    userFind.Authentication.Jwt = string.Empty;
                }

                await _context.SaveChangesAsync();

                return new UserResponseModel
                {
                    Status = true,
                    Message = "Đăng xuất thành công",
                    Token = string.Empty
                };
            }
            catch (Exception ex)
            {

            }

            return new UserResponseModel
            {
                Status = false,
                Message = "Đăng xuất bị lỗi",
                Token = string.Empty
            };
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

        public async Task<UserResponseModel> RefreshToken(UserModel? user)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                var allUsers = await _context.IncludeGetAllUsers();
                var userFind = allUsers.FirstOrDefault(x => x.License == user.License);
                if (userFind is null)
                    throw new ArgumentNullException(nameof(userFind));

                bool isCompare = userFind.CompareDateTime();
                if (!isCompare)
                {
                    return new UserResponseModel
                    {
                        Status = false,
                        Message = "Hết hạn sử dụng",
                        Token = user.Authentication.Jwt
                    };
                }

                string tokenAuthentication = _tokenService.GenerateToken(userFind);
                userFind.Authentication.Jwt = tokenAuthentication;

                await _context.SaveChangesAsync();

                return new UserResponseModel
                {
                    Status = true,
                    Message = "Thành công",
                    Token = tokenAuthentication
                };
            }
            catch (Exception ex) { }

            return new UserResponseModel
            {
                Status = false,
                Message = "Thất bại",
                Token = user.Authentication.Jwt
            };
        }
    }
}
