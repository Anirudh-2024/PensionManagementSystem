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
           
            try
            {
                var existingRecord = await _appDbContext.BankingDetails.FirstOrDefaultAsync(u => u.PensionerId == bankingDetails.PensionerId);
                if (existingRecord != null)
                {
                    throw new BankingExceptions(BankingExceptions.ErrorType.DuplicateRecord, "An Duplicate record already exist with same pensioner id");
                }
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
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void  DeleteBankingDetailsById(Guid bankId)
        {
            
            try
            {
                var result = _appDbContext.BankingDetails.FirstOrDefault(id => id.BankId == bankId);
                if(result == null)
                {
                    throw new BankingExceptions(BankingExceptions.ErrorType.NotFound, "Banking Details not found for given Bank Id.");
                }
                _appDbContext.BankingDetails.Remove(result);
                _appDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
                
            
        }

        public async Task<IEnumerable<BankingDetails>> GetAllBankingDetails()
        {
            
            try
            {
                var result= await _appDbContext.BankingDetails.ToListAsync();
                if(result.Count == 0)
                {
                    throw new BankingExceptions(BankingExceptions.ErrorType.EmptyResult, "There are no guardian details available in databae.");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BankingDetails> GetBankingDetailsById(Guid bankId)
        {
            
            try
            {
                var result= await _appDbContext.BankingDetails.FirstOrDefaultAsync(id => id.BankId == bankId);
                if(result == null)
                {
                    throw new BankingExceptions(BankingExceptions.ErrorType.NotFound, "Banking Details not Found for given Id.");
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BankingDetails> UpdateBankingDetailsById(Guid bankId ,BankingDetails bankingDetails)
        {
            
            try
            {
                var result = await _appDbContext.BankingDetails.FirstOrDefaultAsync(id => id.BankId == bankId);
                if(result == null)
                {
                    throw new BankingExceptions(BankingExceptions.ErrorType.NotFound, "Guardian details not found for given Banking Id.");
                }
                result.PanNumber = bankingDetails.PanNumber;
                result.AccountNumber = bankingDetails.AccountNumber;
                result.BranchName = bankingDetails.BranchName;
                result.BankName = bankingDetails.BankName;
                result.IfscCode = bankingDetails.IfscCode;
                await _appDbContext.SaveChangesAsync();
                return result;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Guid?> GetBankDetailsByPensionerId(Guid pensionerId)
        {
            try
            {
                var bankDetails = await _appDbContext.BankingDetails
                                      .FirstOrDefaultAsync(b => b.PensionerId== pensionerId);
                if(bankDetails == null)
                {
                    throw new BankingExceptions(BankingExceptions.ErrorType.NotFound, "BankDetails Not Found for given Pensioner Id.");
                }
                return bankDetails.BankId;

                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}