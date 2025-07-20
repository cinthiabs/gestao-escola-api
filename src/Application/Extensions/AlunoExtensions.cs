using Application.Alunos.Commands.Create;
using Application.Alunos.Commands.Update;
using Application.Alunos.Queries.GetAll;
using Application.Alunos.Queries.GetById;
using Domain.Entities;

namespace Application.Extensions;

public static class AlunoExtensions
{
    public static GetAlunoByIdViewModel ToGetAlunoByIdViewModel(Aluno aluno)
    {
        return new GetAlunoByIdViewModel
        {
            Id = aluno.Id ?? 0,
            Nome = aluno.Nome,
            DataNascimento = aluno.DataNascimento,
            CPF = aluno.CPF,
            Email = aluno.Email
        };
    }

    public static IEnumerable<GetAllAlunosViewModel> ToGetAllAlunosViewModel(IEnumerable<Aluno> alunos)
    {
        return alunos.Select(aluno => new GetAllAlunosViewModel
        {
            Id = aluno.Id ?? 0,
            Nome = aluno.Nome,
            DataNascimento = aluno.DataNascimento,
            CPF = aluno.CPF,
            Email = aluno.Email
        });
    }

    public static GetAllAlunosPaginadoViewModel ToGetAllAlunosViewModel(Paginacao<Aluno> paginacao)
    {
        return new GetAllAlunosPaginadoViewModel
        {
            PaginaAtual = paginacao.PaginaAtual,
            TamanhoPagina = paginacao.TamanhoPagina,
            TotalRegistro = paginacao.TotalRegistro,
            TotalPaginas = (int)Math.Ceiling((double)paginacao.TotalRegistro / paginacao.TamanhoPagina),
            Alunos = ToGetAllAlunosViewModel(paginacao.Registros)
        };
    }

    public static UpdateAlunoViewModel ToUpdateAlunoViewModel(Aluno aluno)
    {
        return new UpdateAlunoViewModel()
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            DataNascimento = aluno.DataNascimento,
            CPF = aluno.CPF,
            Email = aluno.Email
        };
    }
}
