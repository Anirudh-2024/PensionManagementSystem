using Microsoft.AspNetCore.Identity;

namespace PensionManagementUserLoginService.Models.Repository.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
        
    }
}
