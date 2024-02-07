using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PensionManagementUserLoginService.Models.Context
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "92327fd8-20fc-42d8-9bd8-536a746bc3ec";
            var writerRoleId = "ab185359-929b-48ef-85b3-6acb0637dcfd";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id= readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id= writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                    ConcurrencyStamp=writerRoleId
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);


            var adminUserId = "7ace62fb-9191-4678-946c-3de3f42347da";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper()


            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId= adminUserId,
                    RoleId= writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
            

           
        }
    }
    
}
