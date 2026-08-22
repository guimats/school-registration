using System.Net;

namespace SchoolRegistration.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string entityName, object id) : base($"{entityName} com identificador '{id}' não foi encontrado(a).", HttpStatusCode.NotFound)
        {
        }
    }
}
