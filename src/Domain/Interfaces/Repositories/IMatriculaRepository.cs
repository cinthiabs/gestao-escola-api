using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IMatriculaRepository
{
    public Task<bool> CreateMatriculaAsync(Matricula createMatricula, CancellationToken cancellationToken);
    public Task<bool> UpdateMatriculaAsync(Matricula updateMatricula, CancellationToken cancellationToken);
    public Task<bool> DeleteMatriculaAsync(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<Matricula>> GetAllMatriculasAsync(CancellationToken cancellationToken);
    public Task<Matricula> GetByIdMatriculaAsync(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<Matricula>> GetMatriculaByTurmaIdAsync(int? turmaId, CancellationToken cancellationToken);
    public Task<bool> ExisteAlunoNaTurmaAsync(int alunoId, int turmaId, CancellationToken cancellationToken);
    Task<Paginacao<MatriculaDetalhe>> GetAllMatriculaDetalhesAsync(int paginaAtual, int tamanhoPagina, CancellationToken cancellationToken);
}
