using SchoolRegistration.Domain.Entities;
using System;

namespace SchoolRegistration.Tests.Common.Entities
{
    public static class StudentBuilder
    {
        public static Student Build()
        {
            return new Student
            {
                Id = 1,
                Name = "Teste",
                Email = "teste.teste@teste.com",
                Birthdate = DateTime.Now
            };
        }
    }
}
