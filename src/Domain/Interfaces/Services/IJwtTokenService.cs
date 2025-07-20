using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateToken(Administrador admin);
}
