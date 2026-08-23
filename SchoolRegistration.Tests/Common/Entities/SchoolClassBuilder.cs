using SchoolRegistration.Domain.Entities;

namespace SchoolRegistration.Tests.Common.Entities
{
    public static class SchoolClassBuilder
    {
        public static SchoolClass Build()
        {
            return new SchoolClass 
            { 
                Id = 10,
                Name = "Foo",
                Shift = "Manha",
                AvailableSpots = 10,
                TotalSpots = 10
            };

        }
    }
}
