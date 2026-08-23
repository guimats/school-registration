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
                INSERT INTO Matricula (AlunoId, TurmaId, DataMatricula) 
                VALUES (@StudentId, @SchoolClassId, @CreatedAt);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                int generatedId = await connection.QuerySingleAsync<int>(sql, registration);
                registration.Id = generatedId;
                return registration;
            }
        }
    }
}
