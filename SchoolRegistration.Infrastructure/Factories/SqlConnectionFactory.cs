using SchoolRegistration.Domain.Interfaces.Repositories;
using SchoolRegistration.Domain.Interfaces.Factories;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SchoolRegistration.Infrastructure.Factories
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "A connection string não pode ser nula ou vazia.");
            }

            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }

        public void Dispose()
        {
            
        }
    }
}
