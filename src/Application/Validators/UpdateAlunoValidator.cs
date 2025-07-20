using Application.Alunos.Commands.Update;
using FluentValidation;

namespace Application.Validators;

public class UpdateAlunoValidator : AbstractValidator<UpdateAlunoCommand>
{
    public UpdateAlunoValidator()
    {
          RuleFor(x => x.Nome)
              .MinimumLength(3)
              .When(x => !string.IsNullOrEmpty(x.Nome))
              .WithMessage("O nome deve ter no mínimo 3 caracteres.");

          RuleFor(x => x.DataNascimento)
              .NotEmpty()
              .When(x => x.DataNascimento != null)
              .WithMessage("A data de nascimento é obrigatória.");

          RuleFor(x => x.CPF)
              .Length(11)
              .When(x => !string.IsNullOrEmpty(x.CPF))
              .WithMessage("O CPF deve conter 11 dígitos.")
              .Matches(@"^\d{11}$")
              .When(x => !string.IsNullOrEmpty(x.CPF))
              .WithMessage("O CPF deve conter apenas números.");

          RuleFor(x => x.Email)
              .EmailAddress()
              .When(x => !string.IsNullOrEmpty(x.Email))
              .WithMessage("O e-mail deve ser válido.");

          RuleFor(x => x.Senha)
              .MinimumLength(8)
              .When(x => !string.IsNullOrEmpty(x.Senha))
              .WithMessage("A senha deve conter no mínimo 8 caracteres.")
              .Matches("[A-Z]")
              .When(x => !string.IsNullOrEmpty(x.Senha))
              .WithMessage("A senha deve conter ao menos uma letra maiúscula.")
              .Matches("[a-z]")
              .When(x => !string.IsNullOrEmpty(x.Senha))
              .WithMessage("A senha deve conter ao menos uma letra minúscula.")
              .Matches("[0-9]")
              .When(x => !string.IsNullOrEmpty(x.Senha))
              .WithMessage("A senha deve conter ao menos um número.")
              .Matches("[^a-zA-Z0-9]")
              .When(x => !string.IsNullOrEmpty(x.Senha))
              .WithMessage("A senha deve conter ao menos um caractere especial.");
    }
}