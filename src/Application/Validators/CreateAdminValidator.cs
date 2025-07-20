using Application.Admin.Commands.Create;
using FluentValidation;

namespace Application.Validators;

public class CreateAdminValidator : AbstractValidator<CreateAdminCommand>
{
    public CreateAdminValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("O e-mail é obrigatório.")
           .EmailAddress().WithMessage("O e-mail deve ser válido.");

        RuleFor(x => x.Senha)
          .NotEmpty().WithMessage("A senha é obrigatória.")
          .MinimumLength(8).WithMessage("A senha deve conter no mínimo 8 caracteres.")
          .Matches("[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
          .Matches("[a-z]").WithMessage("A senha deve conter ao menos uma letra minúscula.")
          .Matches("[0-9]").WithMessage("A senha deve conter ao menos um número.")
          .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter ao menos um caractere especial.");
    }
}
