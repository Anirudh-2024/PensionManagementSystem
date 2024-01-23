using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Models.Repository.Implementation
{
    public class PensionerRepository : IPensionerRepository
    {
        public Task<PensionerDetails> AddPensionerDetails(PensionerDetails pensionerDetails)
        {
            throw new NotImplementedException();
        }

        public Task<PensionerDetails> DeletePensionerDetailsById(Guid pensionerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PensionerDetails>> GetAllPensionerDetails()
        {
            throw new NotImplementedException();
        }

        public Task<PensionerDetails> GetPensionerDetailsById(Guid pensionerId)
        {
            throw new NotImplementedException();
        }

        public Task<PensionerDetails> UpdatePensionerDetails(PensionerDetails pensionerDetails)
        {
            throw new NotImplementedException();
        }
    }
}
