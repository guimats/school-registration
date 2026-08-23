using SchoolRegistration.Domain.Entities;
using System.Collections.Generic;

namespace SchoolRegistration.Domain.DTOs.Responses
{
    public class SchoolClassReportResponse
    {
        public List<SchoolClassReportDTO> Reports { get; set; }
    }
}
