
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PensionManagementUserLoginService.Models.Context;
using PensionManagementUserLoginService.Models.Repository.Interfaces;

namespace PensionManagementUserLoginService.Models.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _appDbContext;
        public UserRepository(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }
        public async Task<UserDetails> AddUser(UserDetails userDetails)
        {


            var result = await _appDbContext.UserDetails.AddAsync(userDetails);
            await _appDbContext.SaveChangesAsync();

            return result.Entity;
        }


        public async Task<UserDetails> DeleteUserById(int userId)
        {
            var result = await _appDbContext.UserDetails.FirstOrDefaultAsync(id => id.UserId == userId);
            if (result != null)
            {
                _appDbContext.UserDetails.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<UserDetails>> GetAllUsers()
        {
            return await _appDbContext.UserDetails.ToListAsync();
        }


        public async Task<UserDetails> GetUserById(int userId)
        {
            return await _appDbContext.UserDetails.FirstOrDefaultAsync(id => id.UserId == userId);
        }

        public async Task<UserDetails> UpdateUser(UserDetails userDetails)
        {
            var result = await _appDbContext.UserDetails.FirstOrDefaultAsync(id => id.UserId == userDetails.UserId);
            if (result != null)
            {
                result.Email = userDetails.Email;
                result.UserName = userDetails.UserName;
                result.Password = userDetails.Password;
                await _appDbContext.SaveChangesAsync();
                return result;

            }
            return null;

        }
    }
}

