using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Domain.Exceptions;
using SchoolRegistration.Domain.Interfaces.Repositories;
using SchoolRegistration.Service.Extensions;
using SchoolRegistration.Service.Extensions.Mapping;
using SchoolRegistration.Service.Interfaces;
using SchoolRegistration.Service.Validators.Filters;
using SchoolRegistration.Service.Validators.Requests;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly StudentRequestValidator _requestValidator;
        private readonly StudentFilterValidator _filterValidator;

        public StudentService(IStudentRepository repository, StudentRequestValidator requestValidator, StudentFilterValidator filterValidator)
        {
            _repository = repository;
            _requestValidator = requestValidator;
            _filterValidator = filterValidator;
        }

        public async Task<StudentResponse> AddStudent(StudentRequest request)
        {
            if (request == null)
            {
                throw new ValidationException("Dados da requisição inválidos.");
            }

            var result = _requestValidator.Validate(request);

            if (result.IsValid == false)
            {
                throw new ValidationException(result.JoinErrors());
            }

            var student = request.MapToStudent();

            student = await _repository.AddStudent(student);

            var response = student.MapToResponse();

            return response;
        }

        public async Task DeleteStudent(int id)
        {
            var student = await _repository.GetById(id);

            if (student == null)
            {
                throw new NotFoundException("Aluno não localizado.");
            }

            await _repository.DeleteStudent(id);
        }

        public async Task<StudentResponse> GetStudent(int id)
        {
            var student = await _repository.GetById(id);

            if (student == null)
            {
                throw new NotFoundException("Aluno não localizado.");
            }

            var response = student.MapToResponse();

            return response;
        }

        public async Task UpdateStudent(StudentRequest request, int id)
        {
            if (request == null)
            {
                throw new ValidationException("Dados da requisição inválidos.");
            }

            var result = _requestValidator.Validate(request);

            if (result.IsValid == false)
            {
                throw new ValidationException(result.JoinErrors());
            }

            var student = await _repository.GetById(id);

            if (student == null)
            {
                throw new NotFoundException("Aluno não localizado.");
            }

            student = request.MapToStudent(student);

            await _repository.UpdateStudent(student);
        }

        public async Task<StudentFilterResponse> FilterStudents(StudentFilterRequest filter)
        {
            var result = _filterValidator.Validate(filter);

            if (result.IsValid == false)
            {
                throw new ValidationException(result.JoinErrors());
            }

            var students = await _repository.FilterStudents(filter);
            int count = await _repository.CountFilteredStudents(filter);

            var response = new StudentFilterResponse
            {
                TotalStudents = count,
                Page = filter.Page,
                Students = students.Select(s => s.MapToResponse()).ToList()
            };

            return response;
        }
    }
}
