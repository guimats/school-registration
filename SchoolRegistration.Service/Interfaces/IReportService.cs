using SchoolRegistration.Domain.DTOs.Responses;
using System.Threading.Tasks;

namespace SchoolRegistration.Service.Interfaces
{
    public interface IReportService
    {
        Task<SchoolClassReportResponse> GetSchoolClassReport();
    }
}
