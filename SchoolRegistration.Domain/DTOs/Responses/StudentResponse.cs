using System;

namespace SchoolRegistration.Domain.DTOs.Responses
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
