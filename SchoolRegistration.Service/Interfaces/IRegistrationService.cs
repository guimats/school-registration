using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.DTOs.Responses;
using System.Threading.Tasks;

namespace SchoolRegistration.Service.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationResponse> AddRegistration(RegistrationRequest request);
    }
}
