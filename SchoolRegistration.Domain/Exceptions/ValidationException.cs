using System.Net;

namespace SchoolRegistration.Domain.Exceptions
{
    public class ValidationException : DomainException
    {
        public ValidationException(string message) : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
