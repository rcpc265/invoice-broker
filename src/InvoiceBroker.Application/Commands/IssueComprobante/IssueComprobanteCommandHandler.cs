using InvoiceBroker.Domain.Entities;
using InvoiceBroker.Domain.Repositories;
using InvoiceBroker.Domain.ValueObjects;
using MediatR;

using InvoiceBroker.Application.Common.Interfaces;

namespace InvoiceBroker.Application.Commands.IssueComprobante;

public class IssueComprobanteCommandHandler : IRequestHandler<IssueComprobanteCommand, IssueComprobanteResult>
{
    private readonly IComprobanteRepository _repository;
    private readonly ISunatService _sunatService;

    public IssueComprobanteCommandHandler(IComprobanteRepository repository, ISunatService sunatService)
    {
        _repository = repository;
        _sunatService = sunatService;
    }

    public async Task<IssueComprobanteResult> Handle(IssueComprobanteCommand request, CancellationToken cancellationToken)
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
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        // Simular entrega a SUNAT
        await _sunatService.SendAsync(comprobante, cancellationToken);

        return new IssueComprobanteResult
        {
            ComprobanteId = comprobante.Id,
            Serie = comprobante.Serie.Value,
            Correlativo = comprobante.Correlativo.Value,
            Status = "Accepted"
        };
    }
}
