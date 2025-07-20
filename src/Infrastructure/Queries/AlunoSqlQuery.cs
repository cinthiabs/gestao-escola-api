namespace Infrastructure.Queries;

internal static class AlunoSqlQuery
{
    internal const string InsertAluno = @"
         INSERT INTO Aluno (Nome, DataNascimento, CPF, Email, SenhaHash, SenhaSalt)
         VALUES (@Nome, @DataNascimento, @CPF, @Email, @SenhaHash, @SenhaSalt);
        
        SELECT SCOPE_IDENTITY() AS Id;
    ";

    internal const string SelectAluno = @"
        SELECT * FROM aluno (NOLOCK)
    ";

    internal const string SelectByIdAluno = @"
        SELECT * FROM aluno (NOLOCK) WHERE id = @Id
    ";

    internal const string DeleteAluno = @"
        DELETE FROM aluno WHERE id = @Id
    ";
    internal const string ExisteAlunoRegistrado = @"
        SELECT COUNT(*) FROM aluno (NOLOCK) WHERE CPF = @CPF AND Email = @Email
    ";

}
