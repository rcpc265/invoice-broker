using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.Repositories;
using InvoiceBroker.Domain.ValueObjects;
using MediatR;

using InvoiceBroker.Application.Common.Interfaces;

namespace InvoiceBroker.Application.Commands.IssueComprobante;

public class IssueComprobanteCommandHandler : IRequestHandler<IssueComprobanteCommand, Guid>
{
    private readonly IComprobanteRepository _repository;
    private readonly ISunatService _sunatService;

    public IssueComprobanteCommandHandler(IComprobanteRepository repository, ISunatService sunatService)
    {
        _repository = repository;
        _sunatService = sunatService;
    }

    public async Task<Guid> Handle(IssueComprobanteCommand request, CancellationToken cancellationToken)
    {
        Serie serie = new Serie(request.Serie);
        Correlativo correlativo = new Correlativo(request.Correlativo);
        Moneda moneda = new Moneda(request.Moneda);
        RucEmisor rucEmisor = new RucEmisor(request.RucEmisor);
        Monto subTotal = new Monto(request.SubTotal);

        Comprobante comprobante = new Comprobante(
            Guid.NewGuid(),
            serie,
            correlativo,
            moneda,
            rucEmisor,
            subTotal
        );

        await _repository.AddAsync(comprobante);
        
        // Simular entrega a SUNAT
        await _sunatService.SendAsync(comprobante, cancellationToken);

        return comprobante.Id;
    }
}
