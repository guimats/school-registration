namespace SchoolRegistration.Domain.Entities
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Shift { get; set; }
        public int TotalSpots { get; set; }
        public int AvailableSpots { get; set; }
    }
}
