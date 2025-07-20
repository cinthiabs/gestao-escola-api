using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Application;

public static class DependencyInjectionSetup
{
    public static void AddMediatRApi(this IServiceCollection services)
    {
        services.AddMediatR(m => m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(CreateAlunoValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(CreateTurmaValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(UpdateAlunoValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(UpdateTurmaValidator).Assembly);   
    }
}
