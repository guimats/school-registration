using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Domain.Interfaces.Repositories;
using SchoolRegistration.Service.Extensions.Mapping;
using SchoolRegistration.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Service.Services
{
    public class SchoolClassService : ISchoolClassService
    {
        private readonly ISchoolClassRepository _repository;

        public SchoolClassService(ISchoolClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<SchoolClassLongResponse> GetSchoolClasses()
        {
            var classes = await _repository.GetClasses();

            var response = new SchoolClassLongResponse
            {
                SchoolClasses = classes.Select(c => c.MapToShortResponse()).ToList()
            };

            return response;
        }
    }
}
