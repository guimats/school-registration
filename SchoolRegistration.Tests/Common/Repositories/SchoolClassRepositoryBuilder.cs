using Moq;
using SchoolRegistration.Domain.Entities;
using SchoolRegistration.Domain.Interfaces.Repositories;

namespace SchoolRegistration.Tests.Common.Repositories
{
    public class SchoolClassRepositoryBuilder
    {
        private readonly Mock<ISchoolClassRepository> _repository = new Mock<ISchoolClassRepository>();

        public SchoolClassRepositoryBuilder(SchoolClass schoolClass)
        {
            if (schoolClass != null)
            {
                _repository
                    .Setup(repo => repo.GetById(schoolClass.Id))
                    .ReturnsAsync(schoolClass);
            }
        }

        public Mock<ISchoolClassRepository> Build() => _repository;
    }
}
