using Manager.Api.Data;
using Manager.Shared.Contracts;
using Manager.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Manager.Api.Services
{
    public class ListUserManagerService : IListUserManager
    {
        private DbSet<UserModel> _users => _context.Users;
        private readonly ServerDbContext _context;

        public ListUserManagerService(ServerDbContext context)
        {
            _context = context;
        }

        public async Task<ListUserResponseModel> AddAsync(UserModel? user)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                await _users.AddAsync(user);
                await _context.SaveChangesAsync();

                return new ListUserResponseModel
                {
                    Status = true,
                    Message = "Thêm tài khoản thành công",
                };
            }
            catch (Exception ex) { }

            return new ListUserResponseModel
            {
                Status = false,
                Message = "Thêm tài khoản thất bại"
            };
        }

        public async Task<ListUserResponseModel> AddAsync(List<UserModel?>? users)
        {
            try
            {
                if (users is null)
                    throw new ArgumentNullException(nameof(users));

                int error = 0;
                foreach (var user in users)
                {
                    if (user is null)
                    {
                        ++error;
                        continue;
                    }

                    var resAddAsync = await AddAsync(user);

                    if (!resAddAsync.Status)
                        ++error;
                }

                //danh sách lỗi == danh sách đầu vào => lỗi
                if (users.Count == error)
                    throw new Exception();

                return new ListUserResponseModel
                {
                    Status = true,
                    Message = $"Thêm danh sách tài khoản thành công [Thành Công: {users.Count - error} | Thất Bại: {error}]",
                };
            }
            catch (Exception ex) { }

            return new ListUserResponseModel
            {
                Status = false,
                Message = $"Thêm danh sách tài khoản thất bại [Thất Bại: {users?.Count}]"
            };
        }

        public async Task<ListUserResponseModel> DeleteAsync(int? id)
        {
            try
            {
                if (id is null)
                    throw new ArgumentNullException(nameof(id));

                var userFind = await FindAsync(id);
                if (userFind is null)
                    throw new ArgumentNullException(nameof(userFind));

                _users.Remove(userFind);
                await _context.SaveChangesAsync();

                return new ListUserResponseModel
                {
                    Status = true,
                    Message = "Xóa tài khoản thành công"
                };
            }
            catch (Exception ex) { }

            return new ListUserResponseModel
            {
                Status = false,
                Message = "Xóa tài khoản thất bại"
            };
        }

        public async Task<ListUserResponseModel> DeleteAsync(List<int?>? ids)
        {
            try
            {
                if (ids is null)
                    throw new ArgumentNullException(nameof(ids));

                int error = 0;
                foreach (var id in ids)
                {
                    if (id is null)
                    {
                        ++error;
                        continue;
                    }

                    var resDeleteAsync = await DeleteAsync(id);

                    if (!resDeleteAsync.Status)
                        ++error;
                }

                //danh sách lỗi == danh sách đầu vào => lỗi
                if (ids.Count == error)
                    throw new Exception();

                return new ListUserResponseModel
                {
                    Status = true,
                    Message = $"Xóa danh sách tài khoản thành công [Thành Công: {ids.Count - error} | Thất Bại: {error}]",
                };
            }
            catch (Exception ex) { }

            return new ListUserResponseModel
            {
                Status = false,
                Message = $"Xóa danh sách tài khoản thất bại [Thất Bại: {ids?.Count}]"
            };
        }

        public async Task<UserModel?> FindAsync(int? id)
        {
            try
            {
                if (id is null)
                    throw new ArgumentNullException(nameof(id));

                var userFind = await _users.FindAsync(id);
                return userFind;
            }
            catch (Exception) { }

            return null;
        }

        public async Task<List<UserModel?>?> GetAllAsync()
        {
            var users = await _context.GetUsersWithRolesAndProducts().ToListAsync();
            return users;
        }

        public async Task<ListUserResponseModel> UpdateAsync(UserModel? user)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                int? id = user.Id;
                var userFind = await FindAsync(id);
                if (userFind is null)
                    throw new ArgumentNullException(nameof(userFind));

                userFind.Username = user.Username;
                userFind.Password = user.Password;
                userFind.TimeActive = user.TimeActive;
                userFind.TimeExpired = user.TimeExpired;
                userFind.Role = user.Role;
                userFind.Product = user.Product;

                await _context.SaveChangesAsync();

                return new ListUserResponseModel
                {
                    Status = true,
                    Message = "Cập nhật tài khoản thành công"
                };
            }
            catch (Exception) { }

            return new ListUserResponseModel
            {
                Status = false,
                Message = "Cập nhật tài khoản thất bại"
            };
        }

        public async Task<ListUserResponseModel> UpdateAsync(List<UserModel?>? users)
        {
            try
            {
                if (users is null)
                    throw new ArgumentNullException(nameof(users));

                int error = 0;
                foreach (var user in users)
                {
                    if (user is null)
                    {
                        ++error;
                        continue;
                    }

                    var resUpdateAsync = await UpdateAsync(user);
                    if (!resUpdateAsync.Status)
                        ++error;
                }

                //danh sách lỗi == danh sách đầu vào => lỗi
                if (users.Count == error)
                    throw new Exception();

                return new ListUserResponseModel
                {
                    Status = true,
                    Message = $"Cập nhật danh sách tài khoản thành công [Thành Công: {users.Count - error} | Thất Bại: {error}]",
                };
            }
            catch (Exception) { }

            return new ListUserResponseModel
            {
                Status = false,
                Message = $"Cập nhật danh sách tài khoản thất bại [Thất Bại: {users?.Count}]"
            };
        }
    }
}
