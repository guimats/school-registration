using Dapper;
using SchoolRegistration.Domain.Entities;
using SchoolRegistration.Domain.Interfaces.Factories;
using SchoolRegistration.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Infrastructure.Repositories
{
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SchoolClassRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<SchoolClass> GetById(int id)
        {

            const string sql = @"
                SELECT 
                    Id AS Id,
                    Nome AS Name,
                    Periodo AS Shift,
                    VagasTotal AS TotalSpots,
                    VagasDisponiveis AS AvailableSpots
                FROM Turma
                WHERE Id = @id";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                var schoolClass = await connection.QuerySingleOrDefaultAsync<SchoolClass>(sql, new { id });

                return schoolClass;
            }
        }

        public async Task<List<SchoolClass>> GetClasses()
        {
            const string sql = @"
                SELECT 
                    Id AS Id,
                    Nome AS Name,
                    Periodo AS Shift,
                    VagasTotal AS TotalSpots,
                    VagasDisponiveis AS AvailableSpots
                FROM Turma;";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                var classes = await connection.QueryAsync<SchoolClass>(sql);

                return classes.ToList();
            }
        }
    }
}
