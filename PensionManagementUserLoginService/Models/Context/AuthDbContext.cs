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
            string readerRole = "Reader";
            string writingRole = "Writer";
            base.OnModelCreating(builder);
            var readerRoleId = Guid.NewGuid().ToString(); ;
            var writerRoleId = Guid.NewGuid().ToString(); ;

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id= readerRoleId,
                    Name=readerRole,
                    NormalizedName=readerRole.ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id= writerRoleId,
                    Name=writingRole,
                    NormalizedName=writingRole.ToUpper(),
                    ConcurrencyStamp=writerRoleId
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);


            var adminUserId = Guid.NewGuid().ToString();
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
