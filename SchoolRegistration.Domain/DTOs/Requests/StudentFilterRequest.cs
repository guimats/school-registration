namespace SchoolRegistration.Domain.DTOs.Requests
{
    public class StudentFilterRequest
    {
        public string Name { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
