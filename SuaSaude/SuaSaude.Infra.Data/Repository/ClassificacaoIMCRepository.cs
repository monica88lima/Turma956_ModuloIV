using Dapper;
using MySqlConnector;
using SuaSaude.Service.Entity;
using SuaSaude.Service.Interface;

namespace SuaSaude.Infra.Data.Repository
{
    public class ClassificacaoIMCRepository : IClassificacaoIMCRepository
    {
        private string _stringConnection;
        public ClassificacaoIMCRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }

        public List<ClassificacaoIMCEntity> ConsultaClassificacaoIMC()
        {
            string query = "SELECT * FROM ClassificacaoIMC";

            using MySqlConnection conn = new(_stringConnection);

            return conn.Query<ClassificacaoIMCEntity>(query).ToList();
        }
    }
}
