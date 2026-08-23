using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Domain.Exceptions;
using SchoolRegistration.Domain.Interfaces.Repositories;
using SchoolRegistration.Service.Extensions;
using SchoolRegistration.Service.Extensions.Mapping;
using SchoolRegistration.Service.Interfaces;
using SchoolRegistration.Service.Validators.Requests;
using System.Threading.Tasks;

namespace SchoolRegistration.Service.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolClassRepository _classRepository;
        private readonly RegistrationRequestValidator _validator;

        public RegistrationService(
            IRegistrationRepository registrationRepository,
            IStudentRepository studentRepository,
            ISchoolClassRepository classRepository, 
            RegistrationRequestValidator validator)
        {
            _registrationRepository = registrationRepository;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _validator = validator;
        }

        public async Task<RegistrationResponse> AddRegistration(RegistrationRequest request)
        {
            await Validate(request);

            var registration = request.MapToRegistration();

            registration = await _registrationRepository.AddRegistration(registration);

            if (registration.Id == 0)
            {
                throw new BusinessRuleException("A turma esgotou as vagas durante o processamento da matrícula.");
            }

            var response = registration.MapToResponse();

            return response;
        }

        private async Task Validate(RegistrationRequest request)
        {
            if (request == null)
            {
                throw new ValidationException("Dados da requisição inválidos.");
            }

            var result = _validator.Validate(request);

            if (result.IsValid == false)
            {
                throw new ValidationException(result.JoinErrors());
            }

            var schoolClass = await _classRepository.GetById(request.SchoolClassId);

            if (schoolClass.AvailableSpots <= 0)
            {
                throw new BusinessRuleException("Turma não tem mais vagas disponíveis.");
            }

            var student = await _studentRepository.GetById(request.StudentId);

            if (student.Active)
            {
                throw new BusinessRuleException("Aluno não está ativo.");
            }

            bool isRegistered = await _registrationRepository.IsStudentAlreadyRegistered(request.SchoolClassId, request.StudentId);

            if (isRegistered)
            {
                throw new BusinessRuleException("Aluno já está matriculado na turma");
            }
        }
    }
}
