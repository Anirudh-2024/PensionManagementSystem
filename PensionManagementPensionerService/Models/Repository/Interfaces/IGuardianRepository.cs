namespace PensionManagementPensionerService.Models.Repository.Interfaces
{
    public interface IGuardianRepository
    {
        Task<IEnumerable<GuardianDetails>> GetAllGuardians();

        Task<GuardianDetails> GetGuardianById(Guid guardianId);

        Task<GuardianDetails> AddGuardian(GuardianDetails guardianDetails);

        Task<GuardianDetails> UpdateGuardian(GuardianDetails guardianDetails);

        Task<GuardianDetails> DeleteGuardian(Guid guardianId);
    }
}
