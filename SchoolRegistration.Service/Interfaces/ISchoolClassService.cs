using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Domain.Entities;
using System.Threading.Tasks;

namespace SchoolRegistration.Service.Interfaces
{
    public interface ISchoolClassService
    {
        Task<SchoolClassLongResponse> GetSchoolClasses();
    }
}
