namespace PensionManagementPensionerService.Models.Repository.Interfaces
{
    public interface IPensionerRepository
    {
        Task<IEnumerable<PensionerDetails>> GetAllPensionerDetails();
        Task<PensionerDetails> GetPensionerDetailsById(Guid pensionerId);

        Task<PensionerDetails> UpdatePensionerDetailsById(Guid pensionerid, PensionerDetails pensionerDetails);

        Task<PensionerDetails> AddPensionerDetails(PensionerDetails pensionerDetails);  

        void DeletePensionerDetailsById(Guid pensionerId);
            
    }
}
