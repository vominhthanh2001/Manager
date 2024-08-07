using Manager.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Manager.Api.Data
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options)
        {
            CreateDatabaseIfNotExists();
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<AuthenticationModel> Authentications { get; set; }

        public async Task<List<UserModel>> IncludeGetAllUsers() => await Users.IncludeAll(this).ToListAsync();
        public async Task<List<ProductModel>> IncludeGetAllProducts() => await Products.IncludeAll(this).ToListAsync();
        public async Task<List<RoleModel>> IncludeGetAllRoles() => await Roles.IncludeAll(this).ToListAsync();
        public async Task<List<AuthenticationModel>> IncludeGetAllAuthentications() => await Authentications.IncludeAll(this).ToListAsync();

        private void CreateDatabaseIfNotExists()
        {
            try
            {
                // Kiểm tra và tạo cơ sở dữ liệu và các bảng nếu chưa tồn tại
                if (Database.GetService<IRelationalDatabaseCreator>().Exists())
                {
                    // Database đã tồn tại
                    Console.WriteLine("Database already exists.");
                }
                else
                {
                    // Tạo database và các bảng
                    Database.EnsureCreated();
                    Console.WriteLine("Database created.");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
