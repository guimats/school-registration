using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Domain.Entities;
using System.Collections.Generic;

namespace SchoolRegistration.Service.Extensions.Mapping
{
    public static class ReportExtensions
    {
        public static SchoolClassReportResponse MapToClassResponse(this List<SchoolClassReportDTO> reports)
        {
            return new SchoolClassReportResponse
            {
                Reports = reports
            };

        }
    }
}
