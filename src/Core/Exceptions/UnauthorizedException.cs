namespace Core.Exceptions
{
    /// <summary>
    /// Represents unauthorized exception class.
    /// </summary>
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}

