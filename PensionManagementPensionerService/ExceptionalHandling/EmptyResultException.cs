namespace PensionManagementPensionerService.ExceptionalHandling
{
    public class EmptyResultException:Exception
    {
        public EmptyResultException() :base(){ }
        public EmptyResultException(string message) : base(message) { }

        public EmptyResultException( string message , Exception innerException ) : base(message , innerException ) { }
    }
}
