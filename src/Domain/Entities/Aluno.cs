using Domain.Extensions;

namespace Domain.Entities;

public class Aluno
{
    public int? Id { get; set; }
    public string Nome { get; set; } = default!;
    public DateTime DataNascimento { get; set; } = default!;
    public string CPF { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string SenhaHash { get; set; } = default!;
    public string SenhaSalt { get; set; } = default!;

    public Aluno() { }

    public Aluno(string nome, DateTime dataNascimento, string cpf, string email, string senhaHash, string senhaSalt, int? id = null)
    {
        Id = id;
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
        Email = email;
        SenhaHash = senhaHash;
        SenhaSalt = senhaSalt;
    }

    public static Aluno Criar(string nome, DateTime dataNascimento, string cpf, string email, string senha)
    {
        var geraHashSalt = StringExtensions.GerarHashESalt(senha);
        var hash = geraHashSalt["senhaHash"]; 
        var salt = geraHashSalt["senhaSalt"];

        return new Aluno (
            nome: nome.Trim(),
            dataNascimento: dataNascimento,
            cpf: cpf.Trim(),
            email: email.Trim(),
            senhaHash: hash,
            senhaSalt: salt
        );
    }

    public void Atualizar(string? nome, DateTime dataNascimento, string? cpf, string? email, string? senha = null)
    {
        Nome = nome?.Trim();
        DataNascimento = dataNascimento!;
        CPF = cpf?.Trim();
        Email = email?.Trim();

        if (!string.IsNullOrEmpty(senha))
        {
            var geraHashSalt = StringExtensions.GerarHashESalt(senha);
            SenhaHash = geraHashSalt["senhaHash"];
            SenhaSalt = geraHashSalt["senhaSalt"];
        }
    }
}
