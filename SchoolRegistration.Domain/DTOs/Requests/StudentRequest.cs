using System;

namespace SchoolRegistration.Domain.DTOs.Requests
{
    public class StudentRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
