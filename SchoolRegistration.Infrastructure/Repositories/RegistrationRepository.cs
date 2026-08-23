using Dapper;
using SchoolRegistration.Domain.Entities;
using SchoolRegistration.Domain.Interfaces.Factories;
using SchoolRegistration.Domain.Interfaces.Repositories;
using System.Data;
using System.Threading.Tasks;

namespace SchoolRegistration.Infrastructure.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RegistrationRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Registration> AddRegistration(Registration registration)
        {
            const string sql = @"
                BEGIN TRANSACTION;
                UPDATE Turma
                SET VagasDisponiveis = VagasDisponiveis - 1
                WHERE Id = @SchoolClassId AND VagasDisponiveis > 0;

                IF @@ROWCOUNT > 0
                BEGIN
                    INSERT INTO Matricula (AlunoId, TurmaId, DataMatricula)
                    VALUES (@StudentId, @SchoolClassId, @CreatedAt);
                    COMMIT TRANSACTION;
                    SELECT CAST(SCOPE_IDENTITY() AS int);
                END
                ELSE
                BEGIN
                    ROLLBACK TRANSACTION;
                    SELECT CAST(0 AS int);
                END
                ";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                int generatedId = await connection.QuerySingleAsync<int>(sql, registration);
                registration.Id = generatedId;
                return registration;
            }
        }

        public async Task<bool> IsStudentAlreadyRegistered(int classId, int studentId)
        {
            const string sql = @"
            SELECT CASE 
                WHEN EXISTS (
                    SELECT 1 
                    FROM Matricula 
                    WHERE AlunoId = @StudentId AND TurmaId = @SchoolClassId
                ) THEN 1 
            ELSE 0 
            END;";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                bool isRegistered = await connection.ExecuteScalarAsync<bool>(sql, new { StudentId = studentId, SchoolClassId = classId });

                return isRegistered;
            }
        }
    }
}
