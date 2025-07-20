using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Base;
using Infrastructure.Queries;
using System.Data;

namespace Infrastructure.Repositories;

public class TurmaRepository(DbContext dbContext) : ITurmaRepository
{
    private readonly DbContext dbContext = dbContext;

    public async Task<bool> CreateTurmaAsync(Turma createTurma, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Nome", createTurma.Nome.Trim(), DbType.String);
        parameters.Add("@Descricao", createTurma.Descricao, DbType.String);

        var turmaId = await dbContext.Connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                TurmaSqlQuery.InsertTurma,
                parameters,
                dbContext.Transaction,
                cancellationToken: cancellationToken));

        return turmaId > 0;
    }

    public async Task<IEnumerable<Turma>> GetAllTurmasAsync(CancellationToken cancellationToken)
    {
        return (await dbContext.Connection.QueryAsync<Turma>(
           new CommandDefinition(
               TurmaSqlQuery.SelectTurma,
               cancellationToken: cancellationToken))).ToList();
    }

    public async Task<Turma?> GetByIdTurmaAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Connection.QueryFirstOrDefaultAsync<Turma>(
            new CommandDefinition(
                TurmaSqlQuery.SelectByIdTurma,
                new { Id = id },
                cancellationToken: cancellationToken));
    }

    public async Task<bool> UpdateTurmaAsync(Turma updateTurma, CancellationToken cancellationToken)
    {
        var setClauses = new List<string>();
        var parameters = new DynamicParameters();
        parameters.Add("@Id", updateTurma.Id, DbType.Int32);

        if (!string.IsNullOrWhiteSpace(updateTurma.Nome))
        {
            setClauses.Add("Nome = @Nome");
            parameters.Add("@Nome", updateTurma.Nome.Trim(), DbType.String);
        }

        if (!string.IsNullOrWhiteSpace(updateTurma.Descricao))
        {
            setClauses.Add("Descricao = @Descricao");
            parameters.Add("@Descricao", updateTurma.Descricao.Trim(), DbType.String);
        }

        if (!setClauses.Any())
            return false;

        var sql = $@" UPDATE Turma SET {string.Join(", ", setClauses)}  WHERE Id = @Id";

        var update = await dbContext.Connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                parameters,
                dbContext.Transaction,
                cancellationToken: cancellationToken));

        return update > 0;
    }

    public async Task<bool> DeleteTurmaAsync(int id, CancellationToken cancellationToken)
    {
        var delete = await dbContext.Connection.ExecuteAsync(
            new CommandDefinition(TurmaSqlQuery.DeleteTurma,
            new { Id = id },
            cancellationToken: cancellationToken));

        return delete > 0;
    }

    public async Task<Paginacao<Turma>> GetAllTurmasPaginadosAsync(string nome, int paginaAtual, int tamanhoPagina, CancellationToken cancellationToken)
    {
        var query = TurmaSqlQuery.SelectTurma;
        var parameters = new DynamicParameters();
        if (!string.IsNullOrWhiteSpace(nome))
        {
            query += " WHERE Nome LIKE @Nome";
            parameters.Add("@Nome", $"%{nome.Trim()}%", DbType.AnsiString, size: 200);
        }

        var count = $"SELECT COUNT(*) FROM ({query}) AS Registros";
        var totalRegistro = await dbContext.Connection.ExecuteScalarAsync<int>(
            new CommandDefinition(count, parameters, cancellationToken: cancellationToken));

        int offset = (paginaAtual - 1) * tamanhoPagina;
        query += $" ORDER BY Nome OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        parameters.Add("@Offset", offset, DbType.Int32);
        parameters.Add("@PageSize", tamanhoPagina, DbType.Int32);

        var turmas = await dbContext.Connection.QueryAsync<Turma>(
            new CommandDefinition(query, parameters, cancellationToken: cancellationToken));

        return new Paginacao<Turma>
        {
            PaginaAtual = paginaAtual,
            TamanhoPagina = tamanhoPagina,
            TotalRegistro = totalRegistro,
            Registros = turmas.ToList()
        };
    }
}