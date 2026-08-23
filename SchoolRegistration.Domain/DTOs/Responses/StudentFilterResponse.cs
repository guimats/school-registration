using System.Collections.Generic;

namespace SchoolRegistration.Domain.DTOs.Responses
{
    public class StudentFilterResponse
    {
        public int TotalStudents { get; set; }
        public List<StudentResponse> Students { get; set; }
        public int Page { get; set; }
    }
}
