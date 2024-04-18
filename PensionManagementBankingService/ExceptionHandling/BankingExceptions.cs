namespace PensionManagementBankingService.ExceptionHandling
{
    public class BankingExceptions: Exception
    {
        public enum ErrorType
        {
            DuplicateRecord,
            EmptyResult,
            NotFound
        }
        public ErrorType Type { get; }
        public BankingExceptions(ErrorType type):base()
        {
                Type = type;
        }
        public BankingExceptions(ErrorType type, string message):base(message) 
        {
            Type = type;
        }
        public BankingExceptions(ErrorType type, string message, Exception innerException):base(message, innerException) 
        { 
            Type = type;
        }
      



    }
}
