using Dapper;
using SchoolRegistration.Domain.DTOs.Requests;
using SchoolRegistration.Domain.Entities;
using SchoolRegistration.Domain.Interfaces.Factories;
using SchoolRegistration.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public StudentRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public async Task<Student> GetById(int id)
        {
            const string sql = @"
                SELECT 
                    Id AS Id,
                    Nome AS Name,
                    Email AS Email,
                    DataNascimento AS Birthdate,
                    Ativo AS Active,
                    DataCadastro AS CreatedAt
                FROM Aluno
                WHERE Id = @id";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                Student student = await connection.QuerySingleOrDefaultAsync<Student>(sql, new { id });

                return student;
            }
        }

        public async Task<Student> AddStudent(Student student)
        {
            const string sql = @"
                INSERT INTO Aluno (Nome, Email, DataNascimento, Ativo, DataCadastro) 
                VALUES (@Name, @Email, @Birthdate, @Active, @CreatedAt);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                int generatedId = await connection.QuerySingleAsync<int>(sql, student);
                student.Id = generatedId;
                return student;                
            }
        }

        public async Task DeleteStudent(int id)
        {
            const string sql = @"UPDATE Aluno SET Ativo = 0 WHERE Id = @id;";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, id);

                return;
            }
        }

        public async Task UpdateStudent(Student student)
        {
            const string sql = @"UPDATE Aluno SET Nome = @Name, Email = @Email, DataNascimento = @Birthdate, Ativo = @Active WHERE Id = @Id;";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(sql, student);

                return;
            }
        }

        public async Task<int> CountFilteredStudents(StudentFilterRequest filter)
        {
            const string sql = @"
                SELECT 
                    COUNT(*)
                FROM Aluno
                WHERE (@Name IS NULL OR Nome LIKE '%' + @Name + '%')";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                string nameParam = string.IsNullOrWhiteSpace(filter?.Name) ? null : filter.Name;
                int count = await connection.QuerySingleAsync<int>(sql, new { Name = nameParam });

                return count;
            }
        }

        public async Task<List<Student>> FilterStudents(StudentFilterRequest filter)
        {
            const string sql = @"
                SELECT 
                    Id AS Id,
                    Nome AS Name,
                    Email AS Email,
                    DataNascimento AS Birthdate,
                    Ativo AS Active,
                    DataCadastro AS CreatedAt
                FROM Aluno
                WHERE (@Name IS NULL OR Nome LIKE '%' + @Name + '%')";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                string nameParam = string.IsNullOrWhiteSpace(filter?.Name) ? null : filter.Name;
                var students = await connection.QueryAsync<Student>(sql, new { Name = nameParam });

                return students.ToList();
            }
        }
    }
}
