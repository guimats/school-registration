using SchoolRegistration.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolRegistration.Domain.Interfaces.Repositories
{
    public interface IReportRepository
    {
        Task<List<SchoolClassReportDTO>> GetSchoolClassReport();
    }
}
