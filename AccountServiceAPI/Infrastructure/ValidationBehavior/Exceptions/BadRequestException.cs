using System;
namespace AccountServiceAPI.Infrastructure.ValidationBehavior.Exceptions
{
    public class BadRequestException : Exception
    { 
        public BadRequestException(string message) : base(message) { }
    }
}
