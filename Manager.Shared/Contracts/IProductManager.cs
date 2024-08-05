using Manager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Contracts
{
    public interface IProductManager
    {
        Task<bool> CreateAsync(string? name);
        Task<bool> DeleteAsync(int? id);
        Task<ProductModel?> FindAsync(int? id);
        Task<ProductModel?> FindAsync(string? name);
        Task<List<ProductModel>?> GetAllAsync();
    }
}
