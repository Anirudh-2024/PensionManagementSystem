namespace PensionManagementUserLoginService.Models.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDetails>> GetAllUsers();
        Task<UserDetails> GetUserById(int userId);
        Task<UserDetails> AddUser(UserDetails userDetails);
        Task<UserDetails> UpdateUserById(int userId,UserDetails userDetails);
       void DeleteUserById(int userId);
    }
}
