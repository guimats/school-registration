using System;
using System.Data;

namespace SchoolRegistration.Domain.Interfaces
{
    public interface IDbConnectionFactory : IDisposable
    {
        IDbConnection CreateConnection();
    }
}
