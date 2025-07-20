using System.Data;
using System.Threading;
using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Base;
using Infrastructure.Queries;

namespace Infrastructure.Repositories;

public class MatriculaRepository(DbContext dbContext) : IMatriculaRepository
{
    private readonly DbContext dbContext = dbContext;
    public async Task<bool> CreateMatriculaAsync(Matricula createMatricula, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@AlunoId", createMatricula.AlunoId, DbType.Int32);
        parameters.Add("@TurmaId", createMatricula.TurmaId, DbType.Int32);
       
        return await dbContext.Connection.ExecuteAsync(
            new CommandDefinition(
                MatriculaSqlQuery.CreateMatricula,
                parameters,
                cancellationToken: cancellationToken)) > 0;
    }

    public async Task<bool> DeleteMatriculaAsync(int id, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id, DbType.Int32);
        return await dbContext.Connection.ExecuteAsync(
            new CommandDefinition(
                MatriculaSqlQuery.DeleteMatricula,
                parameters,
                cancellationToken: cancellationToken)) > 0;
    }

    public Task<bool> ExisteAlunoNaTurmaAsync(int alunoId, int turmaId, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@AlunoId", alunoId, DbType.Int32);
        parameters.Add("@TurmaId", turmaId, DbType.Int32);
        return dbContext.Connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                MatriculaSqlQuery.ExistsMatricula,
                parameters,
                cancellationToken: cancellationToken));
    }

    public async Task<Paginacao<MatriculaDetalhe>> GetAllMatriculaDetalhesAsync(int paginaAtual, int tamanhoPagina, CancellationToken cancellationToken)
    {
        var query = MatriculaSqlQuery.GetMatriculaDetails;
        var parameters = new DynamicParameters();

        var count = $"SELECT COUNT(*) FROM ({query}) AS Registros";
        var totalRegistro = await dbContext.Connection.ExecuteScalarAsync<int>(
            new CommandDefinition(count, parameters, cancellationToken: cancellationToken));

        int offset = (paginaAtual - 1) * tamanhoPagina;
        query += $" ORDER BY m.DataMatricula OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        parameters.Add("@Offset", offset, DbType.Int32);
        parameters.Add("@PageSize", tamanhoPagina, DbType.Int32);

        var matriculas = await dbContext.Connection.QueryAsync<MatriculaDetalhe>(
            new CommandDefinition(query, parameters, cancellationToken: cancellationToken));

        return new Paginacao<MatriculaDetalhe>
        {
            PaginaAtual = paginaAtual,
            TamanhoPagina = tamanhoPagina,
            TotalRegistro = totalRegistro,
            Registros = matriculas.ToList()
        };
    }
   
    public async Task<IEnumerable<Matricula>> GetAllMatriculasAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Connection.QueryAsync<Matricula>(
            new CommandDefinition(
                MatriculaSqlQuery.GetAllMatriculas,
                cancellationToken: cancellationToken));
    }

    public async Task<Matricula?> GetByIdMatriculaAsync(int id, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id, DbType.Int32);

        return await dbContext.Connection.QueryFirstOrDefaultAsync<Matricula?>(
            new CommandDefinition(
                MatriculaSqlQuery.GetByIdMatricula,
                parameters,
                cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<Matricula>> GetMatriculaByTurmaIdAsync(int? turmaId, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TurmaId", turmaId, DbType.Int32);

        var resultado =  await dbContext.Connection.QueryAsync<Matricula>(
            new CommandDefinition(
                MatriculaSqlQuery.GetMatriculaByTurmaId,
                parameters,
                cancellationToken: cancellationToken));

        return resultado ?? Enumerable.Empty<Matricula>();
    }

    public async Task<bool> UpdateMatriculaAsync(Matricula updateMatricula, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", updateMatricula.Id, DbType.Int32);
        parameters.Add("@AlunoId", updateMatricula.AlunoId, DbType.Int32);
        parameters.Add("@TurmaId", updateMatricula.TurmaId, DbType.Int32);

        return await dbContext.Connection.ExecuteAsync(
            new CommandDefinition(
                MatriculaSqlQuery.UpdateMatricula,
                parameters,
                cancellationToken: cancellationToken)) > 0;
    }

}
