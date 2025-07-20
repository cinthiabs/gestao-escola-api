using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Base;
using Infrastructure.Queries;
using System.Data;

namespace Infrastructure.Repositories;

public class AlunoRepository(DbContext dbContext) : IAlunoRepository
{
    private readonly DbContext dbContext = dbContext;

    public async Task<bool> CreateAlunoAsync(Aluno createAluno, CancellationToken cancellationToken)
    {

        var parameters = new DynamicParameters();
        parameters.Add("@Nome", createAluno.Nome.Trim(), DbType.AnsiString, size: 100);
        parameters.Add("@DataNascimento", createAluno.DataNascimento, DbType.DateTime);
        parameters.Add("@CPF", createAluno.CPF.Trim(), DbType.AnsiString, size: 11);
        parameters.Add("@Email", createAluno.Email.Trim(), DbType.AnsiString, size: 100);
        parameters.Add("@SenhaHash", createAluno.SenhaHash, DbType.AnsiString, size:250);
        parameters.Add("@SenhaSalt", createAluno.SenhaSalt, DbType.AnsiString, size: 250);
        
        var alunoId = await dbContext.Connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                AlunoSqlQuery.InsertAluno,
                parameters,
                dbContext.Transaction,
                cancellationToken: cancellationToken));
        
        return alunoId > 0;    
        
    }

    public async Task<bool> DeleteAlunoAsync(int id, CancellationToken cancellationToken)
    {
        var delete = await dbContext.Connection.ExecuteAsync(
            new CommandDefinition(AlunoSqlQuery.DeleteAluno,
            new { Id = id },
            cancellationToken: cancellationToken));

        return delete > 0;
    }

    public async Task<bool> ExisteAlunoRegistrado(string cpf, string email, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CPF", cpf.Trim(), DbType.AnsiString, size: 11);
        parameters.Add("@Email", email.Trim(), DbType.AnsiString, size: 100);

        return await dbContext.Connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(AlunoSqlQuery.ExisteAlunoRegistrado, parameters, cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<Aluno>> GetAllAlunosAsync(CancellationToken cancellationToken)
    {
        return (await dbContext.Connection.QueryAsync<Aluno>(
           new CommandDefinition(
               AlunoSqlQuery.SelectAluno,
               cancellationToken: cancellationToken))).ToList();
    }

    public async Task<Paginacao<Aluno>> GetAllAlunosPaginadosAsync(string? nome, int paginaAtual, int tamanhoPagina, CancellationToken cancellationToken)
    {

        var query = AlunoSqlQuery.SelectAluno;
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

        var alunos = await dbContext.Connection.QueryAsync<Aluno>(
            new CommandDefinition(query, parameters, cancellationToken: cancellationToken));

        return new Paginacao<Aluno>
        {
            PaginaAtual = paginaAtual,
            TamanhoPagina = tamanhoPagina,
            TotalRegistro = totalRegistro,
            Registros = alunos.ToList()
        }; 
    }

    public async Task<Aluno?> GetByIdAlunoAsync(int id, CancellationToken cancellationToken)
    {
       return await dbContext.Connection.QueryFirstOrDefaultAsync<Aluno>(
            new CommandDefinition(
               AlunoSqlQuery.SelectByIdAluno,
               new { Id = id },
               cancellationToken: cancellationToken));
    }

    public async Task<bool> UpdateAlunoAsync(Aluno updateAluno, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        var setClauses = new List<string>();

        parameters.Add("@Id", updateAluno.Id, DbType.Int64);

        if (!string.IsNullOrWhiteSpace(updateAluno.Nome))
        {
            setClauses.Add("Nome = @Nome");
            parameters.Add("@Nome", updateAluno.Nome.Trim(), DbType.AnsiString, size: 100);
        }

        if (updateAluno.DataNascimento != null)
        {
            setClauses.Add("DataNascimento = @DataNascimento");
            parameters.Add("@DataNascimento", updateAluno.DataNascimento, DbType.Date);
        }

        if (!string.IsNullOrWhiteSpace(updateAluno.CPF))
        {
            setClauses.Add("CPF = @CPF");
            parameters.Add("@CPF", updateAluno.CPF.Trim(), DbType.AnsiString, size: 11);
        }

        if (!string.IsNullOrWhiteSpace(updateAluno.Email))
        {
            setClauses.Add("Email = @Email");
            parameters.Add("@Email", updateAluno.Email.Trim(), DbType.AnsiString, size: 100);
        }

        if (!string.IsNullOrWhiteSpace(updateAluno.SenhaHash))
        {
            setClauses.Add("SenhaHash = @SenhaHash");
            parameters.Add("@SenhaHash", updateAluno.SenhaHash, DbType.AnsiString, size: 250);
        }

        if (!string.IsNullOrWhiteSpace(updateAluno.SenhaSalt))
        {
            setClauses.Add("SenhaSalt = @SenhaSalt");
            parameters.Add("@SenhaSalt", updateAluno.SenhaSalt, DbType.AnsiString, size: 250);
        }

        if (!setClauses.Any())
            return false;

        var sql = $@" UPDATE aluno SET {string.Join(", ", setClauses)}  WHERE Id = @Id";

        var update = await dbContext.Connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                parameters,
                dbContext.Transaction,
                cancellationToken: cancellationToken));

        return update > 0;
    }

}
