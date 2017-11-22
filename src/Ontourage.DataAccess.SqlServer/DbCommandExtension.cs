using System.Data;

namespace Ontourage.DataAccess.SqlServer
{
    public static class DbCommandExtension
    {
        public static void AddParameter(this IDbCommand command,
            string name, object value)
        {
            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            command.Parameters.Add(param);
        }
    }
}
