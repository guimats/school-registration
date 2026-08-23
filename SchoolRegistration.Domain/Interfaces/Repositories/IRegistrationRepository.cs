using SchoolRegistration.Domain.Entities;
using System.Threading.Tasks;

namespace SchoolRegistration.Domain.Interfaces.Repositories
{
    public interface IRegistrationRepository
    {
        Task<Registration> AddRegistration(Registration registration);
    }
}
