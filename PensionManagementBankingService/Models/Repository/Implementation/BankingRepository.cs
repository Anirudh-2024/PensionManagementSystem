using PensionManagementBankingService.Models.Repository.Interfaces;

namespace PensionManagementBankingService.Models.Repository.Implementation
{
    public class BankingRepository : IBankingRepository
    {
        public Task<BankingDetails> AddBankingDetails(BankingDetails bankingDetails)
        {
            throw new NotImplementedException();
        }



        public Task<BankingDetails> DeleteBankingDetailsById(Guid bankId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BankingDetails>> GetAllBankingDetails()
        {
            throw new NotImplementedException();
        }

        public Task<BankingDetails> GetBankingDetailsById(Guid bankId)
        {
            throw new NotImplementedException();
        }

        public Task<BankingDetails> UpdateBankingDetails(BankingDetails bankingDetails)
        {
            throw new NotImplementedException();
        }
    }
}
