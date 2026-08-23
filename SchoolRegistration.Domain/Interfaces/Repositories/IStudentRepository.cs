using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolRegistration.Domain.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> GetById(int id);

        Task<Student> AddStudent(Student student);

        Task<List<Student>> FilterStudents(StudentFilterRequest filter);

        Task<int> CountFilteredStudents(StudentFilterRequest filter);

        Task UpdateStudent(Student student);

        Task DeleteStudent(int id);
    }
}
