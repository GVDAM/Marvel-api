namespace Marvel.Core.Exceptions
{
    public class InfraException : Exception
    {
        public InfraException(string? message) : base(message)
        {
        }

        public InfraException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
