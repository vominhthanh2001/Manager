using Manager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Shared.Contracts
{
    public interface IToolProductManager
    {
        Task<bool> CreateAsync(string? name);
        Task<bool> DeleteAsync(int? id);
        Task<ToolProductModel?> FindAsync(int? id);
        Task<List<ToolProductModel>?> GetAllAsync();
    }
}
