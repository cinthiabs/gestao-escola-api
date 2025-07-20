using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IAlunoRepository
{
    public Task<bool> CreateAlunoAsync(Aluno createAluno, CancellationToken cancellationToken);
    public Task<bool> UpdateAlunoAsync(Aluno updateAluno, CancellationToken cancellationToken);
    public Task<bool> DeleteAlunoAsync(int id, CancellationToken cancellationToken);
    public Task<bool> ExisteAlunoRegistrado(string cpf, string email, CancellationToken cancellationToken);
    public Task<IEnumerable<Aluno>> GetAllAlunosAsync(CancellationToken cancellationToken);
    public Task<Aluno> GetByIdAlunoAsync(int id, CancellationToken cancellationToken);
    public Task<Paginacao<Aluno>> GetAllAlunosPaginadosAsync(string? nome, int paginaAtual, int tamanhoPagina, CancellationToken cancellationToken);
}
