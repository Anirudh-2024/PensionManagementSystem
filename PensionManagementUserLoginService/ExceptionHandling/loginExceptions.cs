using System.Net;

namespace PensionManagementUserLoginService.ExceptionHandling
{
    public class loginExceptions:Exception
    {
        
        public loginExceptions():base()
        {
               
        }
        public loginExceptions(string message):base(message)
        {
               
        }
        public loginExceptions(HttpStatusCode statusCode,string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
