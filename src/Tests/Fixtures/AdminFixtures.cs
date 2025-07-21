using Application.Admin.Commands.Create;
using Application.Admin.Commands.Auth;

namespace Tests.Fixtures;

public static class AdminFixtures
{
    public static CreateAdminCommand GetValidCreateAdminCommand()
    {
        return new CreateAdminCommand("admin@email.com", "senha12365656");
    }

    public static CreateAdminCommand GetInvalidCreateAdminCommand()
    {
        return new CreateAdminCommand("", "");
    }

    public static AuthAdminCommand GetValidAuthAdminCommand()
    {
        return new AuthAdminCommand("admin@email.com", "senha12365656");
    }

    public static AuthAdminViewModel GetAuthAdminViewModelMock()
    {
        return new AuthAdminViewModel("token12354564675", DateTime.Now.AddHours(1));
    }
}
