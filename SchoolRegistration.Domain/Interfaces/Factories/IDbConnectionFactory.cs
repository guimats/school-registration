using System;
using System.Data;

namespace SchoolRegistration.Domain.Interfaces.Factories
{
    public interface IDbConnectionFactory : IDisposable
    {
        IDbConnection CreateConnection();
    }
}
