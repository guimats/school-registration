using FluentValidation.Results;
using SchoolRegistration.Domain.Exceptions;
using System.Linq;

namespace SchoolRegistration.Service.Extensions
{
    public static class ValidationResultExtensions
    {
        public static string JoinErrors(this ValidationResult result)
        {
            return string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
        }
    }
}
