namespace Infrastructure.Queries;

internal static class AdminSqlQuery
{
    internal const string InsertAdmin = @"
        INSERT INTO Administrador (Email, SenhaHash, SenhaSalt)
        VALUES (@Email, @SenhaHash, @SenhaSalt)
    ";

    internal const string AuthenticateAdmin = @"
        SELECT COUNT(*) FROM Administrador
        WHERE Email = @Email AND SenhaHash = @SenhaHash
    ";

    internal const string ExisteAdmin = @"
        SELECT COUNT(*) FROM Administrador
        WHERE Email = @Email
    ";
    
    internal const string SelectAdminByEmail = @"
        SELECT * FROM Administrador
        WHERE Email = @Email
    ";

    internal const string UpdateAuthAdmin = @"
        UPDATE Administrador
        SET Token = @Token, DataExpiracaoToken = @DataExpiracaoToken
        WHERE Id = @Id
    ";

}
