using Moq;
using SchoolRegistration.Domain.Entities;
using SchoolRegistration.Domain.Interfaces.Repositories;
using System;

namespace SchoolRegistration.Tests.Common.Repositories
{
    internal class StudentRepositoryBuilder
    {
        private readonly Mock<IStudentRepository> _repository = new Mock<IStudentRepository>();

        public StudentRepositoryBuilder(Student student = null)
        {
            if (student != null)
            {
                _repository
                    .Setup(repo => repo.GetById(student.Id))
                    .ReturnsAsync(student);
            }
        }

        public Mock<IStudentRepository> Build() => _repository;
    }
}
