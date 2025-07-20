using Domain.Entities;

namespace Application.Extensions;

public static class MatriculaExtensions
{
    public static IEnumerable<GetAllMatriculasViewModel> ToGetAllMatriculasViewModel(IEnumerable<MatriculaDetalhe> matriculas)
    {
        return matriculas.Select(matricula => new GetAllMatriculasViewModel
        {
            Id = matricula.Id,
            Aluno = matricula.Aluno,
            Turma = matricula.Turma,
            DataMatricula = matricula.DataMatricula,
        });
    }

    public static GetAllMatriculasPaginadoViewModel ToGetAllMatriculasPaginadoViewModel(Paginacao<MatriculaDetalhe> paginacao)
    {
        return new GetAllMatriculasPaginadoViewModel
        {
            PaginaAtual = paginacao.PaginaAtual,
            TamanhoPagina = paginacao.TamanhoPagina,
            TotalRegistro = paginacao.TotalRegistro,
            TotalPaginas = (int)Math.Ceiling((double)paginacao.TotalRegistro / paginacao.TamanhoPagina),
            Matriculas = ToGetAllMatriculasViewModel(paginacao.Registros)
        };
    }


}
