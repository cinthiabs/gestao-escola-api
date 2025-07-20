namespace Infrastructure.Queries;

internal static class TurmaSqlQuery
{
    internal const string InsertTurma = @"
        INSERT INTO turma (nome, descricao)
        VALUES (@Nome, @descricao);
        
        SELECT SCOPE_IDENTITY() AS Id;
    ";

    internal const string SelectTurma = @"
        SELECT * FROM turma (NOLOCK)
    ";

    internal const string SelectByIdTurma = @"
        SELECT * FROM turma (NOLOCK) WHERE id = @Id
    ";

    internal const string DeleteTurma = @"
        DELETE FROM turma WHERE id = @Id
    ";

    internal const string UpdateTurma = @"
        UPDATE turma
        SET 
            nome = @Nome,
            descricao = @Descricao
        WHERE id = @Id
    ";
}
