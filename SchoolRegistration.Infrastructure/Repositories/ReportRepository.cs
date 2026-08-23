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
    public class ReportRepository : IReportRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ReportRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<SchoolClassReportDTO>> GetSchoolClassReport()
        {
            const string sql = @"
                SELECT 
                    t.Id AS SchoolClassId,
                    t.Nome AS SchoolClassName,
                    t.VagasTotal AS Capacity,
                    COUNT(m.Id) AS RegisteredStudentsCount,
                    (t.VagasTotal - COUNT(m.Id)) AS RemainingSpots
                FROM Turma AS t
                LEFT JOIN Matricula AS m ON t.Id = m.TurmaId
                GROUP BY 
                    t.Id, t.Nome, t.VagasTotal
                ORDER BY 
                    t.Nome ASC;";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                var report = await connection.QueryAsync<SchoolClassReportDTO>(sql);

                return report.ToList();
            }
        }
    }
}
