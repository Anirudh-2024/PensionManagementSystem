using System.Net;

namespace PensionManagementBankingService.ExceptionHandling
{
    public class BankingExceptions: Exception
    {
       
        public BankingExceptions():base()
        {
                
        }
        public BankingExceptions(string message):base(message) 
        {
            
        }
        public BankingExceptions(HttpStatusCode statusCode,string message, Exception innerException):base(message, innerException) 
        { 
           
        }
      



    }
}
