using Domain.Extensions;

namespace Domain.Entities;

public class Administrador
{
    public int? Id { get; set; }
    public string Email { get; set; } = default!;
    public string SenhaHash { get; set; } = default!;
    public string SenhaSalt { get; set; } = default!;
    public string? Token { get; set; } = default!;
    public DateTime? DataExpiracaoToken { get; set; } = default!;

    public Administrador() { }

    public Administrador(string email, string senhaHash, string senhaSalt, int? id = null)
    {
        Id = id;
        Email = email;
        SenhaHash = senhaHash;
        SenhaSalt = senhaSalt;
    }

    public static Administrador Criar(string email, string senha)
    {
        var geraHashSalt = StringExtensions.GerarHashESalt(senha);
        var hash = geraHashSalt["senhaHash"];
        var salt = geraHashSalt["senhaSalt"];

        return new Administrador(
            email: email.Trim(),
            senhaHash: hash,
            senhaSalt: salt
        );
    }
}