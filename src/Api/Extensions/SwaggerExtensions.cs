using Microsoft.OpenApi.Models;

namespace Api.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerUi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        const string Bearer = "Bearer";

        services.AddSwaggerGen(c =>
        {

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "gestao-escola-api",
                Description = "Api responsavel por fazer a gestão de turmas e alunos de uma escola",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Cinthia Barbosa",
                    Email = "cinthiabarbosa8d@outlook.com",
                }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                    Description = "Cabeçalho de autorização JWT usando o Bearer. Exemplo: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Bearer
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = Bearer
                            },
                            Scheme = "oauth2",
                            Name = Bearer,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}
