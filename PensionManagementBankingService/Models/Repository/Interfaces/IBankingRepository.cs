namespace PensionManagementBankingService.Models.Repository.Interfaces
{
    public interface IBankingRepository
    {
        Task<IEnumerable<BankingDetails>> GetAllBankingDetails();
        Task<BankingDetails> GetBankingDetailsById(Guid bankId);
        Task<BankingDetails> UpdateBankingDetailsById(Guid bankId,BankingDetails bankingDetails);
        Task<BankingDetails> AddBankingDetails(BankingDetails bankingDetails);
        void DeleteBankingDetailsById(Guid bankId);

        Task<Guid?> GetBankDetailsByPensionerId(Guid pensionerId);

        
    }
}
