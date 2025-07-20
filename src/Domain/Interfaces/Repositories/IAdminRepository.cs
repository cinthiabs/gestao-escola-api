using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IAdminRepository
{
    Task<bool> CreateAdminAsync(Administrador admin, CancellationToken cancellationToken);
    Task<bool> ExisteAdminAsync(string Email, CancellationToken cancellationToken);
    Task<bool> AuthenticateAdminAsync(string Email, string senha, CancellationToken cancellationToken);
    Task<bool> UpdateAuthAdminAsync(int? id, string token, DateTime dataExpiracao, CancellationToken cancellationToken);
    Task<Administrador> GetAdminByEmailAsync(string Email, CancellationToken cancellationToken);
}