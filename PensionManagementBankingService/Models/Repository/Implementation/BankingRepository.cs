using Microsoft.EntityFrameworkCore;
using PensionManagementBankingService.ExceptionHandling;
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

           

            BankingDetails addbanking = new BankingDetails
            {
                BankId = Guid.NewGuid(),
                BankName = bankingDetails.BankName,
                AccountNumber = bankingDetails.AccountNumber,
                IfscCode = bankingDetails.IfscCode,
                BranchName = bankingDetails.BranchName,
                PanNumber = bankingDetails.PanNumber,
                PensionerId = bankingDetails.PensionerId,
            };

            var result = await _appDbContext.BankingDetails.AddAsync(addbanking);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }


        public void  DeleteBankingDetailsById(Guid bankId)
        {

            var result = _appDbContext.BankingDetails.FirstOrDefault(id => id.BankId == bankId);
            

            _appDbContext.BankingDetails.Remove(result);
            _appDbContext.SaveChanges();


        }

        public async Task<IEnumerable<BankingDetails>> GetAllBankingDetails()
        {

            var result = await _appDbContext.BankingDetails.ToListAsync();

            return result;
        }

        public async Task<BankingDetails> GetBankingDetailsById(Guid bankId)
        {

            var result = await _appDbContext.BankingDetails.FirstOrDefaultAsync(id => id.BankId == bankId);

            return result;
        }

        public async Task<BankingDetails> UpdateBankingDetailsById(Guid bankId ,BankingDetails bankingDetails)
        {

            var result = await _appDbContext.BankingDetails.FirstOrDefaultAsync(id => id.BankId == bankId);

            result.PanNumber = bankingDetails.PanNumber;
            result.AccountNumber = bankingDetails.AccountNumber;
            result.BranchName = bankingDetails.BranchName;
            result.BankName = bankingDetails.BankName;
            result.IfscCode = bankingDetails.IfscCode;
            await _appDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<Guid?> GetBankDetailsByPensionerId(Guid pensionerId)
        {
            var bankDetails = await _appDbContext.BankingDetails
                                      .FirstOrDefaultAsync(b => b.PensionerId == pensionerId);

            return bankDetails.BankId;
        }
    }
}