namespace Tests.Fixtures;

public static class MatriculaFixtures
{
    public static CreateMatriculaCommand GetValidCreateMatriculaCommand()
    {
        return new CreateMatriculaCommand(1, 1);
    }

    public static CreateMatriculaCommand GetInvalidCreateMatriculaCommand()
    {
        return new CreateMatriculaCommand(0, 0);
    }

    public static GetAllMatriculaQuery GetAllMatriculaQueryMock()
    {
        return new GetAllMatriculaQuery(1, 10);
    }

    public static GetAllMatriculasPaginadoViewModel GetAllMatriculasPaginadoViewModelMock()
    {
        return new GetAllMatriculasPaginadoViewModel(
            1,
            10,
            1,
            1,
            new List<GetAllMatriculasViewModel> {
                new GetAllMatriculasViewModel(1, "Aluno 1" , "Turma 1", DateTime.Now)
            }
        );
    }
}
