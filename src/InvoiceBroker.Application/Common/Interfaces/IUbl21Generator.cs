using InvoiceBroker.Domain.Entities;

namespace InvoiceBroker.Application.Common.Interfaces;

public interface IUbl21Generator
{
    string GenerateInvoiceXml(Comprobante comprobante);
}
