using SchoolRegistration.Domain.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace SchoolRegistration.API.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is DomainException domainException)
            {
                object errorResponse = new
                {
                    statusCode = (int)domainException.StatusCode,
                    message = domainException.Message
                };

                context.Response = context.Request.CreateResponse(domainException.StatusCode, errorResponse);
                return;
            }

            // Erros inesperados de infra/banco continuam caindo aqui (500)
            base.OnException(context);
        }
    }
}
