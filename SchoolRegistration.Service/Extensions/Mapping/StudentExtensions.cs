using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Domain.Entities;

namespace SchoolRegistration.Service.Extensions.Mapping
{
    public static class StudentExtensions
    {
        public static Student MapToStudent(this StudentRequest request)
        {
            return new Student
            {
                Name = request.Name,
                Email = request.Email,
                Birthdate = request.Birthdate
            };
        }

        public static StudentResponse MapToResponse(this Student student)
        {
            return new StudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Birthdate = student.Birthdate,
                Active = student.Active,
                CreatedAt = student.CreatedAt
            };
        }

        public static Student MapToStudent(this StudentRequest request, Student student)
        {
            student.Name = request.Name;
            student.Email = request.Email;
            student.Birthdate = request.Birthdate;

            return student;
        }
    }
}
