using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Domain.Interfaces.Repositories;
using SchoolRegistration.Service.Extensions.Mapping;
using SchoolRegistration.Service.Interfaces;
using System.Threading.Tasks;

namespace SchoolRegistration.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<SchoolClassReportResponse> GetSchoolClassReport()
        {
            var report = await _repository.GetSchoolClassReport();

            return report.MapToClassResponse();
        }
    }
}
