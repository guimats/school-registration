namespace SchoolRegistration.Domain.DTOs.Responses
{
    public class SchoolClassShortResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Shift { get; set; }
        public int TotalSpots { get; set; }
        public int AvailableSpots { get; set; }
    }
}
