using System;
using System.Net;

namespace SchoolRegistration.Domain.Exceptions
{
    public class BusinessRuleException : DomainException
    {
        public BusinessRuleException(string message) : base(message, HttpStatusCode.Conflict)
        {
        }
    }
}
