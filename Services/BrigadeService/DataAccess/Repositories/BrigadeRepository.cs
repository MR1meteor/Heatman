﻿using BrigadeService.DataAccess.Repositories.Interfaces;
using BrigadeService.DataAccess.Repositories.Sql.Brigade;
using BrigadeService.Models.Db;
using Shared.Dapper;
using Shared.Dapper.Interfaces;

namespace BrigadeService.DataAccess.Repositories;

public class BrigadeRepository : IBrigadeRepository
{
    private readonly IDapperContext _dapperContext;

    public BrigadeRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<Guid?> CreateTodayAsync()
    {
        var parameters = new
        {
            CreationDate = DateTime.UtcNow
        };

        return await _dapperContext.CommandWithResponse<Guid>(new QueryObject(SqlScripts.Insert, parameters));
    }

    public async Task<Guid?> GetTodayByEmployeeIdsAsync(IEnumerable<Guid> employeeIds)
    {
        var parameters = new
        {
            EmployeeIds = employeeIds.ToArray(),
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddDays(1),
        };

        return await _dapperContext.FirstOrDefault<Guid>(new QueryObject(SqlScripts.GetTodayByEmployeeIds, parameters));
    }
}