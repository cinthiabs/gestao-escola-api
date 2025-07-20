using Domain.Interfaces.Base;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Base;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjectionSetup
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(x => new DbContext(ConfigureConnection()));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddService();
        services.AddRepository();
    }

    public static void AddRepository(this IServiceCollection services)
    {
       services.AddScoped<IAlunoRepository, AlunoRepository>();
       services.AddScoped<ITurmaRepository, TurmaRepository>();
       services.AddScoped<IMatriculaRepository, MatriculaRepository>();
       services.AddScoped<IAdminRepository, AdminRepository>();
    }

    private static string ConfigureConnection()
    {
        var conn = Environment.GetEnvironmentVariable("DefaultConnection");
        return conn;
    }

    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
    }
}
