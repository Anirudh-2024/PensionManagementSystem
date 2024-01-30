using Microsoft.EntityFrameworkCore;
using PensionManagementBankingService.Models.Context;
using PensionManagementBankingService.Models.Repository.Interfaces;


namespace PensionManagementBankingService.Models.Repository.Implementation
{
    public class BankingRepository : IBankingRepository
    {

        private readonly AppDBContext _appDbContext;
        public BankingRepository(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }

        public async Task<BankingDetails> AddBankingDetails(BankingDetails bankingDetails)
        {

            var result = await _appDbContext.BankingDetails.AddAsync(bankingDetails);
            await _appDbContext.SaveChangesAsync();
            return bankingDetails;
        }


        public void  DeleteBankingDetailsById(Guid bankId)
        {
            var result =_appDbContext.BankingDetails.FirstOrDefault(id => id.BankId == bankId);
          
                _appDbContext.BankingDetails.Remove(result);
                _appDbContext.SaveChanges();
                
            
        }

        public async Task<IEnumerable<BankingDetails>> GetAllBankingDetails()
        {
            return await _appDbContext.BankingDetails.ToListAsync();
        }

        public async Task<BankingDetails> GetBankingDetailsById(Guid bankId)
        {
            return await _appDbContext.BankingDetails.FirstOrDefaultAsync(id => id.BankId == bankId);
        }

        public async Task<BankingDetails> UpdateBankingDetailsById(Guid bankId ,BankingDetails bankingDetails)
        {
            var result = await _appDbContext.BankingDetails.FirstOrDefaultAsync(id => id.BankId == bankId);
            if (result != null)
            {
                result.PanNumber = bankingDetails.PanNumber;
                result.AccountNumber = bankingDetails.AccountNumber;
                result.BranchName = bankingDetails.BranchName;
                result.BankName = bankingDetails.BankName;
                result.IfscCode = bankingDetails.IfscCode;
                await _appDbContext.SaveChangesAsync();
                return result;

            }
            return null;
        }
    }
}