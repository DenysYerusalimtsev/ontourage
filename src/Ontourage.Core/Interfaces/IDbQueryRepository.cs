using Ontourage.Core.Entities.Sql;

namespace Ontourage.Core.Interfaces
{
    public interface IDbQueryRepository
    {
        SelectResult ExecuteQuery(string query);

        void ExecuteNonQuery(string commandText);
    }
}
