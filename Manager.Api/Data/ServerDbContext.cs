using Manager.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager.Api.Data
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options) { Database.EnsureCreated(); }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ToolProductModel> ToolProducts { get; set; }
    }
}
