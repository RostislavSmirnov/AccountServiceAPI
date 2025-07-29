namespace AccountServiceAPI.Infrastructure.ValidationBehavior.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
