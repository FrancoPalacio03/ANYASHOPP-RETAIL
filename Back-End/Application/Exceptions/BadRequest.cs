namespace Application.Exceptions
{
    public class BadRequest : Exception
    {
        public BadRequest(string message) : base(message)
        {
        }

        public BadRequest(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
