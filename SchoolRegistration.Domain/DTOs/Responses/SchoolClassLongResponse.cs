using System.Collections.Generic;

namespace SchoolRegistration.Domain.DTOs.Responses
{
    public class SchoolClassLongResponse
    {
        public List<SchoolClassShortResponse> SchoolClasses { get; set; }
    }
}
