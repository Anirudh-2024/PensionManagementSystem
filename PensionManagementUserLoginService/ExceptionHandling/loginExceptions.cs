namespace PensionManagementUserLoginService.ExceptionHandling
{
    public class loginExceptions:Exception
    {
        public enum ErrorType
        {
            EmptyResult,
            NotFound
        }
        public ErrorType Type { get; }
        public loginExceptions(ErrorType type):base()
        {
                Type = type;
        }
        public loginExceptions(ErrorType type, string message):base(message)
        {
                Type= type;
        }
        public loginExceptions(ErrorType type, string message, Exception innerException) : base(message, innerException)
        {
            Type = type;
        }
    }
}
