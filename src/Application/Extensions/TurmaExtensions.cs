using Application.Turmas.Commands.Create;
using Application.Turmas.Commands.Update;
using Application.Turmas.Queries.GetAll;
using Application.Turmas.Queries.GetById;
using Domain.Entities;

namespace Application.Extensions;

public static class TurmaExtensions
{
    public static IEnumerable<CreateTurmaCommand> ToCreateTurmaCommands(IEnumerable<Turma> turmas)
    {
        return turmas.Select(turma => new CreateTurmaCommand(turma.Nome, turma.Descricao));
    }

    public static Turma ToTurma(this CreateTurmaCommand command)
    {
        return new Turma(command.Nome, command.Descricao);
    }

    public static UpdateTurmaViewModel ToUpdateTurmaViewModel(Turma turma)
    {
        return new UpdateTurmaViewModel(turma.Id ?? 0, turma.Nome, turma.Descricao);
    }

    public static GetTurmaByIdViewModel ToGetTurmaByIdViewModel(this Turma turma)
    {
        return new GetTurmaByIdViewModel(turma.Id ?? 0, turma.Nome, turma.Descricao);
    }

    public static IEnumerable<GetAllTurmasViewModel> ToGetAllTurmasViewModel(IEnumerable<Turma> turmas)
    {
        return turmas.Select(turma => new GetAllTurmasViewModel
        {
            Id = turma.Id ?? 0,
            Nome = turma.Nome,
            Descricao = turma.Descricao,
            QuantidadeAlunos = turma.Matriculas?.Count ?? 0
        });
    }

    public static GetAllTurmasPaginadoViewModel ToGetAllTurmasPaginadoViewModel(Paginacao<Turma> turmas)
    {
        return new GetAllTurmasPaginadoViewModel
        {
            PaginaAtual = turmas.PaginaAtual,
            TamanhoPagina = turmas.TamanhoPagina,
            TotalRegistro = turmas.TotalRegistro,
            TotalPaginas = (int)Math.Ceiling((double)turmas.TotalRegistro / turmas.TamanhoPagina),
            Turmas = ToGetAllTurmasViewModel(turmas.Registros)
        };
    }
}