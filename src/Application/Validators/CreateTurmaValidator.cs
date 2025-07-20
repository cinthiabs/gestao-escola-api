using Application.Turmas.Commands.Create;
using FluentValidation;

namespace Application.Validators;

public class CreateTurmaValidator : AbstractValidator<CreateTurmaCommand>
{
    public CreateTurmaValidator()
    {
        RuleFor(command => command.Nome)
            .NotEmpty().WithMessage("Nome da turma é obrigatório.")
            .MinimumLength(3).WithMessage("Nome da turma deve ter no mínimo 3 caracteres.");

        RuleFor(command => command.Descricao)
            .NotEmpty().WithMessage("Descrição da turma é obrigatória.")
            .MinimumLength(3).WithMessage("Descrição da turma deve ter no mínimo 3 caracteres.");
    }
}
