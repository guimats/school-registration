namespace SchoolRegistration.Domain.Entities
{
    public class SchoolClassReportDTO
    {
        public int SchoolClassId { get; set; }
        public string SchoolClassName { get; set; }
        public int Capacity { get; set; }
        public int RegisteredStudentsCount { get; set; }
        public int RemainingSpots { get; set; }
    }
}
