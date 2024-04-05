namespace PensionManagementPensionerService.ExceptionalHandling
{
    public class DuplicateRecordException:Exception
    {
        public DuplicateRecordException() : base() { }
        public DuplicateRecordException(string message) : base(message) { }

        public DuplicateRecordException(string message, Exception innerException) : base(message, innerException) { }
    }
}
