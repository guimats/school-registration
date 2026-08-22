using System;
using System.Net;

namespace SchoolRegistration.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected DomainException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
