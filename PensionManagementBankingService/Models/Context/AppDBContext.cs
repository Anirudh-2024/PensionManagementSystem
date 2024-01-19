using Microsoft.EntityFrameworkCore;
namespace PensionManagementBankingService.Models.Context
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions options):base(options) { }
        public DbSet<BankingDetails> BankingDetails { get; set;} 
    }
}
