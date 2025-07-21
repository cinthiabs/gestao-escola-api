using Application.Turmas.Commands.Create;
using Application.Turmas.Commands.Delete;
using Application.Turmas.Commands.Update;
using Application.Turmas.Queries.GetById;

namespace Tests.Fixtures;

public static class TurmaFixtures
{
    public static CreateTurmaCommand GetValidCreateTurmaCommand()
    {
        return new CreateTurmaCommand(
            "Turma Teste",
            "Descrição da Turma"
        );
    }

    public static CreateTurmaCommand GetInvalidCreateTurmaCommand()
    {
        return new CreateTurmaCommand(
            "",
            ""
        );
    }

    public static DeleteTurmaCommand GetDeleteTurmaCommand(int id = 1)
    {
        return new DeleteTurmaCommand(id);
    }

    public static UpdateTurmaCommand GetUpdateTurmaCommand(int id = 1)
    {
        return new UpdateTurmaCommand(
            id,
            "Turma Atualizada",
            "Descrição Atualizada"
        );
    }

    public static GetTurmaByIdViewModel GetTurmaByIdViewModelMock(int id = 1)
    {
        return new GetTurmaByIdViewModel(id, "Turma Teste", "Descrição da Turma");
    }

    public static UpdateTurmaViewModel GetUpdateTurmaViewModelMock(int id = 1)
    {
        return new UpdateTurmaViewModel(id, "Turma Atualizada", "Descrição Atualizada");
    }
}
