namespace PensionManagementPensionerService.Models.Repository.Interfaces
{
    public interface IGuardianRepository
    {
        Task<IEnumerable<GuardianDetails>> GetAllGuardianDetails();

        Task<GuardianDetails> GetGuardianById(Guid guardianId);

        Task<GuardianDetails> AddGuardian(GuardianDetails guardianDetails);

        Task<GuardianDetails> UpdateGuardian(GuardianDetails guardianDetails);

        Task<GuardianDetails> DeleteGuardianById(Guid guardianId);
    }
}
