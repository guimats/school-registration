using SchoolRegistration.Domain.Entities;

namespace SchoolRegistration.Tests.Common.Entities
{
    public static class RegistrationBuilder
    {
        public static Registration Build()
        {
            return new Registration
            {
                Id = 10,
                StudentId = 1,
                SchoolClassId = 1
            };
        }
    }
}