namespace PensionManagementBankingService.DTO
{
    public class BankRequest
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public string BranchName { get; set; }
        public string PanNumber { get; set; }


        public Guid PensionerId { get; set; }


    }
}
