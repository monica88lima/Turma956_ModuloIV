using System.Data;

namespace ApiClientes.Core.Interface
{
    public interface IConnectionDataBase
    {
        IDbConnection CreateConnection();
    }
}
