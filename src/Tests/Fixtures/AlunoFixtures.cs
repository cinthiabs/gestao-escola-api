using Application.Alunos.Commands.Create;
using Application.Alunos.Commands.Delete;
using Application.Alunos.Commands.Update;
using Application.Alunos.Queries.GetById;

namespace Tests.Fixtures;

public static class AlunoFixtures
{
    public static CreateAlunoCommand GetValidCreateAlunoCommand()
    {
        return new CreateAlunoCommand(
            "Aluno Teste",
            DateTime.Now.AddYears(-20),
            "12345678901",
            "teste@email.com",
            "senha123"
        );
    }

    public static CreateAlunoCommand GetInvalidCreateAlunoCommand()
    {
        return new CreateAlunoCommand(
            "",
            DateTime.Now,
            "",
            "",
            ""
        );
    }

    public static GetAlunoByIdViewModel GetAlunoByIdViewModelMock(int id = 1)
    {
        return new GetAlunoByIdViewModel
        {
            Id = id,
            Nome = "Teste",
            DataNascimento = DateTime.Now,
            CPF = "1233456",
            Email = "aluno@email.com"
        };
    }

    public static DeleteAlunoCommand GetDeleteAlunoCommand(int id = 1)
    {
        return new DeleteAlunoCommand(id);
    }

    public static UpdateAlunoCommand GetUpdateAlunoCommand(int id = 1)
    {
        return new UpdateAlunoCommand(
            id,
            "Teste",
            DateTime.Now.AddYears(-21),
            "12345678901",
            "alunoatualizado@email.com",
            "novasenha123"
        );
    }

    public static UpdateAlunoViewModel GetUpdateAlunoViewModelMock(int id = 1)
    {
        return new UpdateAlunoViewModel
        {
            Id = id,
            Nome = "Aluno Atualizado"
        };
    }
}
