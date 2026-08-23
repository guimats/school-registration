using System;

namespace SchoolRegistration.Domain.DTOs.Responses
{
    public class RegistrationResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SchoolClassId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
