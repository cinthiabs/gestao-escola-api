using Application.Alunos.Commands.Create;
using FluentValidation;

namespace Application.Validators;

public class CreateAlunoValidator : AbstractValidator<CreateAlunoCommand>
{
    public CreateAlunoValidator()
    {
        RuleFor(x => x.Nome)
          .NotEmpty().WithMessage("O nome é obrigatório.")
          .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.");

        RuleFor(x => x.DataNascimento)
          .NotEmpty().WithMessage("A data de nascimento é obrigatória.");

        RuleFor(x => x.CPF)
           .NotEmpty().WithMessage("O CPF é obrigatório.")
           .Length(11).WithMessage("O CPF deve conter 11 dígitos.")
           .Matches(@"^\d{11}$").WithMessage("O CPF deve conter apenas números.");

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
