using EmployeeService.DataAccess.Database.Dapper.Interfaces;

namespace EmployeeService.DataAccess.Database.Dapper;

public class QueryObject : IQueryObject
{
    public QueryObject(string sql, object parameters)
    {
        if (string.IsNullOrEmpty(sql)) throw new ArgumentException("Sql is empty");

        Sql = sql;
        Params = parameters;
    }

    public string Sql { get; }
    public object Params { get; }
}