namespace PensionManagementBankingService.Models.Repository.Interfaces
{
    public interface IBankingRepository
    {
        Task<IEnumerable<BankingDetails>> GetAllBankingDetails();
        Task<BankingDetails> GetBankingDetailsById(Guid bankId);
        Task<BankingDetails> UpdateBankingDetails(BankingDetails bankingDetails);
        Task<BankingDetails> AddBankingDetails(BankingDetails bankingDetails);
        Task<BankingDetails> DeleteBankingDetailsById(Guid bankId);

        
    }
}
