using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Domain.Entities;

namespace SchoolRegistration.Service.Extensions.Mapping
{
    public static class RegistrationExtensions
    {
        public static Registration MapToRegistration(this RegistrationRequest request)
        {
            return new Registration
            {
                SchoolClassId = request.SchoolClassId,
                StudentId = request.StudentId
            };
        }

        public static RegistrationResponse MapToResponse(this Registration registration)
        {
            return new RegistrationResponse
            {
                Id = registration.Id,
                SchoolClassId = registration.SchoolClassId,
                StudentId = registration.StudentId,
                CreatedAt = registration.CreatedAt
            };
        }
    }
}
