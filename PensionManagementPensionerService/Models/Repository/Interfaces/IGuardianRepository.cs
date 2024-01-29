namespace PensionManagementPensionerService.Models.Repository.Interfaces
{
    public interface IGuardianRepository
    {
        Task<IEnumerable<GuardianDetails>> GetAllGuardianDetails();

        Task<GuardianDetails> GetGuardianById(Guid guardianId);

        Task<GuardianDetails> AddGuardian(GuardianDetails guardianDetails);

        Task<GuardianDetails> UpdateGuardianById(Guid guardianId, GuardianDetails guardianDetails);

        void DeleteGuardianById(Guid guardianId);
    }
}
