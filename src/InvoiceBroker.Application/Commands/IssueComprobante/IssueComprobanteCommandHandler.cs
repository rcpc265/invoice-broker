using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.Repositories;
using InvoiceBroker.Domain.ValueObjects;
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
        Serie serie = new Serie(request.Serie);
        Correlativo correlativo = new Correlativo(request.Correlativo);

        Comprobante comprobante = new Comprobante(
            Guid.NewGuid(),
            serie,
            correlativo,
            request.SubTotal
        );

        await _repository.AddAsync(comprobante);

        return comprobante.Id;
    }
}
