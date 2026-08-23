using Moq;
using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.Entities;
using SchoolRegistration.Domain.Exceptions;
using SchoolRegistration.Domain.Interfaces.Repositories;
using SchoolRegistration.Service.Interfaces;
using SchoolRegistration.Service.Services;
using SchoolRegistration.Service.Validators.Requests;
using SchoolRegistration.Tests.Common.Entities;
using SchoolRegistration.Tests.Common.Repositories;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SchoolRegistration.Tests.Services
{
    public class RegistrationServiceTests
    {
        private Mock<IRegistrationRepository> _registrationRepository;
        private Mock<IStudentRepository> _studentRepository;
        private Mock<ISchoolClassRepository> _classRepository;

        [Fact]
        public async Task Registration_WhenStudentIsAlreadyRegistered_Should_Throw()
        {
            var request = new RegistrationRequest
            {
                StudentId = 1,
                SchoolClassId = 10
            };

            var registration = RegistrationBuilder.Build();
            var schoolClass = SchoolClassBuilder.Build();
            var student = StudentBuilder.Build();

            var service = CreateService(registration, schoolClass, student, request);

            var exception = await Should.ThrowAsync<BusinessRuleException>(async () =>{ await service.AddRegistration(request); });

            exception.Message.ShouldBe("Aluno já está matriculado na turma.");

            _registrationRepository.Verify(r => r.AddRegistration(It.IsAny<Registration>()), Times.Never);
        }

        [Fact]
        public async Task Registration_WhenStudentIsNotActive_Should_Throw()
        {
            var request = new RegistrationRequest
            {
                StudentId = 1,
                SchoolClassId = 10
            };

            var registration = RegistrationBuilder.Build();
            var schoolClass = SchoolClassBuilder.Build();
            var student = StudentBuilder.Build();

            student.Active = false;

            var service = CreateService(registration, schoolClass, student);

            var exception = await Should.ThrowAsync<BusinessRuleException>(async () => { await service.AddRegistration(request); });

            exception.Message.ShouldBe("Aluno não está ativo.");

            _registrationRepository.Verify(r => r.AddRegistration(It.IsAny<Registration>()), Times.Never);
        }

        [Fact]
        public async Task Registration_WhenClassHasNoAvailableSpots_Should_Throw()
        {
            var request = new RegistrationRequest
            {
                StudentId = 1,
                SchoolClassId = 10
            };

            var registration = RegistrationBuilder.Build();
            var schoolClass = SchoolClassBuilder.Build();
            var student = StudentBuilder.Build();

            schoolClass.AvailableSpots = 0;

            var service = CreateService(registration, schoolClass, student);

            var exception = await Should.ThrowAsync<BusinessRuleException>(async () => { await service.AddRegistration(request); });

            exception.Message.ShouldBe("Turma não tem mais vagas disponíveis.");

            _registrationRepository.Verify(r => r.AddRegistration(It.IsAny<Registration>()), Times.Never);
        }

        [Fact]
        public async Task Registration_WithoutStudentId_Should_Throw()
        {
            var request = new RegistrationRequest
            {
                SchoolClassId = 10
            };

            var registration = RegistrationBuilder.Build();
            var schoolClass = SchoolClassBuilder.Build();
            var student = StudentBuilder.Build();

            schoolClass.AvailableSpots = 0;

            var service = CreateService(registration, schoolClass, student);

            var exception = await Should.ThrowAsync<ValidationException>(async () => { await service.AddRegistration(request); });

            exception.Message.ShouldBe("Aluno deve ser preenchido");

            _registrationRepository.Verify(r => r.AddRegistration(It.IsAny<Registration>()), Times.Never);
        }

        private IRegistrationService CreateService(Registration registration, SchoolClass schoolClass, Student student, RegistrationRequest classRequest = null)
        {
            _registrationRepository = new RegistrationRepositoryBuilder(registration, classRequest).Build();
            _studentRepository = new StudentRepositoryBuilder(student).Build();
            _classRepository = new SchoolClassRepositoryBuilder(schoolClass).Build();

            return new RegistrationService(
                _registrationRepository.Object,
                _studentRepository.Object,
                _classRepository.Object,
                new RegistrationRequestValidator()
            );
        }
    }
}
