using Manager.Api.Data;
using Manager.Shared.Contracts;
using Manager.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager.Api.Services
{
    public class ToolProductService : IToolProductManager
    {
        private DbSet<ToolProductModel> _product => _contenxt.ToolProducts;
        private readonly ServerDbContext _contenxt;
        public ToolProductService(ServerDbContext contenxt)
        {
            _contenxt = contenxt;
        }

        public async Task<bool> CreateAsync(string? name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException(nameof(name));

                await _product.AddAsync(new ToolProductModel
                {
                    Name = name,
                });
                await _contenxt.SaveChangesAsync();
            }
            catch (Exception ex) { }

            return false;
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                if (id is null)
                    throw new ArgumentNullException(nameof(id));

                var toolProductFind = await FindAsync(id);
                if (toolProductFind is null)
                    return false;

                _product.Remove(toolProductFind);
                await _contenxt.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { }

            return false;
        }

        public async Task<ToolProductModel?> FindAsync(int? id)
        {
            try
            {
                var toolProductFind = await _product.FindAsync(id);
                return toolProductFind;
            }
            catch (Exception ex) { }

            return null;
        }

        public async Task<List<ToolProductModel>?> GetAllAsync()
        {
            try
            {
                var products = await _product.ToListAsync();
                return products;
            }
            catch (Exception ex) { }

            return new List<ToolProductModel>();
        }
    }
}
