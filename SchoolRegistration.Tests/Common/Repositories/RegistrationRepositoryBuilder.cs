using Moq;
using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.Entities;
using SchoolRegistration.Domain.Interfaces.Repositories;

namespace SchoolRegistration.Tests.Common.Repositories
{
    public class RegistrationRepositoryBuilder
    {
        private readonly Mock<IRegistrationRepository> _repository = new Mock<IRegistrationRepository>();

        public RegistrationRepositoryBuilder(Registration registration, RegistrationRequest request = null)
        {
            _repository
                .Setup(r => r.AddRegistration(It.IsAny<Registration>()))
                .ReturnsAsync(registration);

            if (request != null)
            {
                _repository
                    .Setup(r => r.IsStudentAlreadyRegistered(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(true);
            }
        }

        public Mock<IRegistrationRepository> Build () => _repository;
    }
}
