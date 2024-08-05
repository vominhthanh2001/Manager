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
        public DbSet<ProductModel> ToolProducts { get; set; }

        // Phương thức tùy chỉnh để lấy dữ liệu User cùng với Role và Product
        public IQueryable<UserModel> GetUsersWithRolesAndProducts()
        {
            return Users
                .Include(u => u.Role)
                .Include(u => u.Product);
        }

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
