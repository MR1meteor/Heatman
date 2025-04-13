namespace Shared.Dapper.Interfaces;

public interface IQueryObject
{
    public string Sql { get; }
    public object Params { get; }
}