using System;

namespace SchoolRegistration.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
