using PensionManagementUserLoginService.Models.Repository.Interfaces;

namespace PensionManagementUserLoginService.Models.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        public Task<UserDetails> AddUser(UserDetails userDetails)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetails> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDetails>> GetAllUsers()
        {
            throw new NotImplementedException();
        }


        public Task<UserDetails> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetails> UpdateUser(UserDetails userDetails)
        {
            throw new NotImplementedException();
        }
    }
}
