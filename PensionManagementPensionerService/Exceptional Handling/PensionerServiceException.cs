using System.Net;

namespace PensionManagementPensionerService.ExceptionalHandling
{
    public class PensionerServiceException : Exception
    {
        public PensionerServiceException() { }

        public PensionerServiceException(string? message) : base(message) { }

        public PensionerServiceException(HttpStatusCode httpStatusCode ,string? message, Exception innerException) : base(message) { }

    }
}
