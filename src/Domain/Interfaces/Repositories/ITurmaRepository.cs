using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ITurmaRepository
{
    public Task<bool> CreateTurmaAsync(Turma createTurma, CancellationToken cancellationToken);
    public Task<bool> UpdateTurmaAsync(Turma updateTurma, CancellationToken cancellationToken);
    public Task<bool> DeleteTurmaAsync(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<Turma>> GetAllTurmasAsync(CancellationToken cancellationToken);
    public Task<Turma> GetByIdTurmaAsync(int id, CancellationToken cancellationToken);
    public Task<Paginacao<Turma>> GetAllTurmasPaginadosAsync(string nome, int paginaAtual, int tamanhoPagina, CancellationToken cancellationToken);
}
