using System;

namespace SchoolRegistration.Domain.Entities
{
    public class Registration
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SchoolClassId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
