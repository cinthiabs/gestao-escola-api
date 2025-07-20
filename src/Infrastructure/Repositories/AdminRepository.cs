using System.Data;
using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Base;
using Infrastructure.Queries;

namespace Infrastructure.Repositories;

public class AdminRepository(DbContext dbContext) : IAdminRepository
{
    private readonly DbContext dbContext = dbContext;
    public Task<bool> AuthenticateAdminAsync(string email, string senha, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateAdminAsync(Administrador admin, CancellationToken cancellationToken)
    {
          var parameters = new DynamicParameters();
          parameters.Add("@Email", admin.Email.Trim(), DbType.AnsiString, size: 100);
          parameters.Add("@SenhaHash", admin.SenhaHash, DbType.AnsiString, size: 250);
          parameters.Add("@SenhaSalt", admin.SenhaSalt, DbType.AnsiString, size: 250);

          var createAdmin = await dbContext.Connection.ExecuteAsync(
              new CommandDefinition(
                  AdminSqlQuery.InsertAdmin,
                  parameters,
                  dbContext.Transaction,
                  cancellationToken: cancellationToken));

          return createAdmin > 0;      
    }

    public Task<bool> ExisteAdminAsync(string email, CancellationToken cancellationToken)
    {       
        var parameters = new DynamicParameters();
        parameters.Add("@Email", email.Trim(), DbType.AnsiString, size: 100);

        return dbContext.Connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                AdminSqlQuery.ExisteAdmin,
                parameters,
                dbContext.Transaction,
                cancellationToken: cancellationToken));
    }

    public async Task<Administrador?> GetAdminByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Email", email.Trim(), DbType.AnsiString, size: 100);

        return await dbContext.Connection.QueryFirstOrDefaultAsync<Administrador>(
            new CommandDefinition(
                AdminSqlQuery.SelectAdminByEmail,
                parameters,
                cancellationToken: cancellationToken));
    }

  
    public async Task<bool> UpdateAuthAdminAsync(int? id, string token, DateTime dataExpiracao, CancellationToken cancellationToken)
    {

          var parameters = new DynamicParameters();
          parameters.Add("@Id", id, DbType.Int32);
          parameters.Add("@Token", token, DbType.AnsiString);
          parameters.Add("@DataExpiracaoToken", dataExpiracao, DbType.DateTime);

          return await dbContext.Connection.ExecuteAsync(
              new CommandDefinition(
                  AdminSqlQuery.UpdateAuthAdmin,
                  parameters,
                  dbContext.Transaction,
                  cancellationToken: cancellationToken)) > 0;
    }

}
