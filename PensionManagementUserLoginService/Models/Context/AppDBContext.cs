using Microsoft.EntityFrameworkCore;

namespace PensionManagementUserLoginService.Models.Context
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options) { }
        public DbSet<UserDetails> UserDetails { get; set; }
    }
}
