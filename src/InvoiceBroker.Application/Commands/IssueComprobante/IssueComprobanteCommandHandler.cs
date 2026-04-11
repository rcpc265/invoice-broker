using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.Repositories;
using MediatR;

namespace InvoiceBroker.Application.Commands.IssueComprobante;

public class IssueComprobanteCommandHandler : IRequestHandler<IssueComprobanteCommand, Guid>
{
    private readonly IComprobanteRepository _repository;

    public IssueComprobanteCommandHandler(IComprobanteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(IssueComprobanteCommand request, CancellationToken cancellationToken)
    {
        Comprobante comprobante = new Comprobante 
        { 
            Id = Guid.NewGuid(),
            Serie = request.Serie, 
            Correlativo = request.Correlativo,
            SubTotal = request.SubTotal
        };

        comprobante.CalculateTotals();

        await _repository.AddAsync(comprobante);

        return comprobante.Id;
    }
}
