using PensionManagementPensionerService.Models.Repository.Interfaces;

namespace PensionManagementPensionerService.Models.Repository.Implementation
{
    public class PensionerRepository : IPensionerRepository
    {
        public Task<PensionerDetails> AddPensionerDetails(PensionerDetails pensionerDetails)
        {
            throw new NotImplementedException();
        }

        public Task<PensionerDetails> DeletePensionerDetails(Guid pensionerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PensionerDetails>> GetAllPensioners()
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
