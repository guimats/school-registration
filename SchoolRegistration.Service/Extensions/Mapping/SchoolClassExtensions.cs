using SchoolRegistration.Domain.DTOs.Responses;
using SchoolRegistration.Domain.Entities;

namespace SchoolRegistration.Service.Extensions.Mapping
{
    public static class SchoolClassExtensions
    {
        public static SchoolClassShortResponse MapToShortResponse(this SchoolClass schoolClass)
        {
            return new SchoolClassShortResponse
            {
                Id = schoolClass.Id,
                Name = schoolClass.Name,
                Shift = schoolClass.Shift,
                TotalSpots = schoolClass.TotalSpots,
                AvailableSpots = schoolClass.AvailableSpots
            };
        }
    }
}
