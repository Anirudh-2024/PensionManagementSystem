using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Models.Repository.Implementation
{
    public class GuardianRepository : IGuardianRepository
    {
        public Task<GuardianDetails> AddGuardian(GuardianDetails guardianDetails)
        {
            throw new NotImplementedException();
        }

        public Task<GuardianDetails> DeleteGuardianById(Guid guardianId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GuardianDetails>> GetAllGuardianDetails()
        {
            throw new NotImplementedException();
        }

        public Task<GuardianDetails> GetGuardianById(Guid guardianId)
        {
            throw new NotImplementedException();
        }

        public Task<GuardianDetails> UpdateGuardian(GuardianDetails guardianDetails)
        {
            throw new NotImplementedException();
        }
    }
}
