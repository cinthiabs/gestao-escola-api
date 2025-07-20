using Application.Turmas.Commands.Update;
using FluentValidation;

namespace Application.Validators;

public class UpdateTurmaValidator : AbstractValidator<UpdateTurmaCommand>
{
    public UpdateTurmaValidator()
    {
        RuleFor(command => command.Nome)
            .MinimumLength(3)
            .When(command => !string.IsNullOrEmpty(command.Nome))
            .WithMessage("Nome da turma deve ter no mínimo 3 caracteres.");

        RuleFor(command => command.Descricao)
            .MinimumLength(3)
            .When(command => !string.IsNullOrEmpty(command.Descricao))
            .WithMessage("Descrição da turma deve ter no mínimo 3 caracteres.");
    }
}