using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.DTOs.Responses;
using System.Threading.Tasks;

namespace SchoolRegistration.Service.Interfaces
{
    public interface IStudentService
    {
        Task<StudentFilterResponse> FilterStudents(StudentFilterRequest filter);

        Task<StudentResponse> GetStudent(int id);

        Task<StudentResponse> AddStudent(StudentRequest request);

        Task UpdateStudent(StudentRequest request, int id);

        Task DeleteStudent(int id);
    }
}
