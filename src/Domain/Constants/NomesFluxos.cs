namespace Domain.Constants;

public static class NomesFluxos
{
    public const string FLUXO_CREATE_ALUNO = nameof(FLUXO_CREATE_ALUNO);
    public const string FLUXO_DELETE_ALUNO = nameof(FLUXO_DELETE_ALUNO);
    public const string FLUXO_UPDATE_ALUNO = nameof(FLUXO_UPDATE_ALUNO);
    public const string FLUXO_GET_ALL_ALUNOS = nameof(FLUXO_GET_ALL_ALUNOS);
    public const string FLUXO_GET_ID_ALUNO = nameof(FLUXO_GET_ID_ALUNO);
    public const string FLUXO_CREATE_TURMA = nameof(FLUXO_CREATE_TURMA);
    public const string FLUXO_DELETE_TURMA = nameof(FLUXO_DELETE_TURMA);
    public const string FLUXO_UPDATE_TURMA = nameof(FLUXO_UPDATE_TURMA);
    public const string FLUXO_GET_ALL_TURMAS = nameof(FLUXO_GET_ALL_TURMAS);
    public const string FLUXO_GET_ID_TURMA = nameof(FLUXO_GET_ID_TURMA);
    public const string FLUXO_CREATE_ADMIN = nameof(FLUXO_CREATE_ADMIN);
    public const string FLUXO_AUTH_ADMIN = nameof(FLUXO_AUTH_ADMIN);
    public const string FLUXO_CREATE_MATRICULA = nameof(FLUXO_CREATE_MATRICULA);
    public const string FLUXO_DELETE_MATRICULA = nameof(FLUXO_DELETE_MATRICULA);
    public const string FLUXO_GET_ALL_MATRICULAS = nameof(FLUXO_GET_ALL_MATRICULAS);
    public const string FLUXO_GET_ID_MATRICULA = nameof(FLUXO_GET_ID_MATRICULA);
}

public static class NomesErros
{
    public const string ERROR_CREATE = "Erro ao criar novo registro.";
    public const string ERROR_GET = "Erro ao buscar registro.";
    public const string ERROR_GET_ALL = "Erro ao buscar todos os registros.";
    public const string ERROR_UPDATE = "Erro ao atualizar registro.";
    public const string ERROR_DELETE = "Erro ao deletar registro.";

    public const string INVALID_COD_ALUNO = "Código do aluno é inválido.";
    public const string CONFLITO = "Já existe esse usuário cadastrado no sistema.";
    public const string ERROR = "Ocorreu um erro durante a operação.";
    public const string INVALID_CAMPOS_OBRIGATORIOS = "Email e Senha são obrigatorios";
    public const string ERRO_ALUNO_JA_MATRICULADO = "Aluno já matriculado nesta turma.";
    public const string ERRO_ALUNO_NAO_ENCONTRADO = "Aluno não encontrado.";
    public const string ERRO_TURMA_NAO_ENCONTRADA = "Turma não encontrada.";
}

public static class Logs
{
    public const string LOG_ERROR = "Fluxo:{Process} Error:{Error}";
}