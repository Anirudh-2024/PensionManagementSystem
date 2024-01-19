using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace PensionManagementPensionerService.Models.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<PensionerDetails> PensionerDetails { get; set; }
        public DbSet<PensionPlanDetails> PensionPlanDetails { get; set; }
        public DbSet<GuardianDetails> GuardianDetails { get; set;}

    }
}
