namespace PensionManagementUserLoginService.Models.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDetails>> GetAllUsers();
        Task<UserDetails> GetUserById(int userId);
        Task<UserDetails> AddUser(UserDetails userDetails);
        Task<UserDetails> UpdateUser(UserDetails userDetails);
        Task<UserDetails> DeleteUserById(int userId);
    }
}
