using Manager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Contracts
{
    public interface IListUserManager
    {
        Task<ListUserResponseModel> AddAsync(UserModel? user);
        Task<ListUserResponseModel> AddAsync(List<UserModel?>? users);
        Task<ListUserResponseModel> UpdateAsync(UserModel? user);
        Task<ListUserResponseModel> UpdateAsync(List<UserModel?> users);
        Task<ListUserResponseModel> DeleteAsync(int? id);
        Task<ListUserResponseModel> DeleteAsync(List<int?>? ids);
        Task<UserModel?> FindAsync(int? id);
        Task<List<UserModel?>?> GetAllAsync();
    }
}
