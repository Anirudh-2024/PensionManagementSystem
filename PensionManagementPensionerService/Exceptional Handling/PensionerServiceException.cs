namespace PensionManagementPensionerService.ExceptionalHandling
{
    public class PensionerServiceException : Exception
    {
        public PensionerServiceException() { }

        public PensionerServiceException(string? message) : base(message) { }

        public PensionerServiceException(string? message, Exception innerException) : base(message) { }

        public class EmptyResultException : PensionerServiceException
        {
            public EmptyResultException() : base() { }
            public EmptyResultException(string message) : base(message) { }

            public EmptyResultException(string message, Exception innerException) : base(message, innerException) { }
        }
        public class DuplicateRecordException : PensionerServiceException
        {
            public DuplicateRecordException() : base() { }
            public DuplicateRecordException(string message) : base(message) { }

            public DuplicateRecordException(string message, Exception innerException) : base(message, innerException) { }
        }
        public class NotFoundException : PensionerServiceException
        {
            public NotFoundException() : base() { }
            public NotFoundException(string message) : base(message) { }

            public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        }
    }
}
