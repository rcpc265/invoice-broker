using FluentValidation;

namespace InvoiceBroker.Application.Commands.IssueComprobante;

public class IssueComprobanteCommandValidator : AbstractValidator<IssueComprobanteCommand>
{
    public IssueComprobanteCommandValidator()
    {
        RuleFor(v => v.Serie)
            .NotEmpty().WithMessage("Serie es requerida.")
            .Length(4).WithMessage("Serie debe tener exactamente 4 caracteres.");

        RuleFor(v => v.Correlativo)
            .NotEmpty().WithMessage("Correlativo es requerido.")
            .MaximumLength(8).WithMessage("Correlativo no puede tener más de 8 caracteres.");

        RuleFor(v => v.Moneda)
            .NotEmpty().WithMessage("Moneda es requerida.")
            .Length(3).WithMessage("Moneda debe tener exactamente 3 caracteres.");

        RuleFor(v => v.RucEmisor)
            .NotEmpty().WithMessage("RUC del emisor es requerido.")
            .Length(11).WithMessage("RUC del emisor debe tener 11 dígitos.")
            .Matches("^[0-9]+$").WithMessage("RUC del emisor solo debe contener números.");

        RuleFor(v => v.SubTotal)
            .GreaterThanOrEqualTo(0).WithMessage("SubTotal no puede ser negativo.");
    }
}
