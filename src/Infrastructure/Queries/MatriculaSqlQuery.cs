namespace Infrastructure.Queries;

internal static class MatriculaSqlQuery
{
    internal const string CreateMatricula = @"
        INSERT INTO Matricula (AlunoId, TurmaId)
        VALUES (@AlunoId, @TurmaId);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

    internal const string UpdateMatricula = @"
        UPDATE Matricula
        SET AlunoId = @AlunoId, TurmaId = @TurmaId
        WHERE Id = @Id;";
    internal const string ExistsMatricula = "SELECT COUNT(1) FROM Matricula WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId;";

    internal const string DeleteMatricula = "DELETE FROM Matricula WHERE Id = @Id;";

    internal const string GetAllMatriculas = "SELECT * FROM Matricula;";

    internal const string GetByIdMatricula = "SELECT * FROM Matricula WHERE Id = @Id;";

    internal const string GetMatriculaByAlunoId = "SELECT * FROM Matricula WHERE AlunoId = @AlunoId;";

    internal const string GetMatriculaByTurmaId = "SELECT * FROM Matricula WHERE TurmaId = @TurmaId;";

    internal const string GetMatriculaDetails = @"
        SELECT m.Id, a.Nome AS Aluno, t.Nome AS Turma, m.DataMatricula
        FROM Matricula m
        INNER JOIN Aluno (nolock) a ON m.AlunoId = a.Id
        INNER JOIN Turma (nolock) t ON m.TurmaId = t.Id
        ";
}
