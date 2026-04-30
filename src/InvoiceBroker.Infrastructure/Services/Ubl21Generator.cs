using System.Globalization;
using System.Xml.Linq;
using InvoiceBroker.Application.Common.Interfaces;
using InvoiceBroker.Domain.Entities;

namespace InvoiceBroker.Infrastructure.Services;

public class Ubl21Generator : IUbl21Generator
{
    public string GenerateInvoiceXml(Comprobante comprobante)
    {
        XNamespace cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
        XNamespace cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
        XNamespace ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";

        string subTotal = comprobante.SubTotal.Value.ToString("F2", CultureInfo.InvariantCulture);
        string igv = comprobante.Igv.Value.ToString("F2", CultureInfo.InvariantCulture);
        string total = comprobante.Total.Value.ToString("F2", CultureInfo.InvariantCulture);

        var invoice = new XElement("Invoice",
            new XAttribute(XNamespace.Xmlns + "cac", cac),
            new XAttribute(XNamespace.Xmlns + "cbc", cbc),
            new XAttribute(XNamespace.Xmlns + "ext", ext),
            new XElement(ext + "UBLExtensions",
                new XElement(ext + "UBLExtension",
                    new XElement(ext + "ExtensionContent", "") // Placeholder para Firma XMLDSig
                )
            ),
            new XElement(cbc + "UBLVersionID", "2.1"),
            new XElement(cbc + "CustomizationID", "2.0"),
            new XElement(cbc + "ID", $"{comprobante.Serie.Value}-{comprobante.Correlativo.Value}"),
            new XElement(cbc + "IssueDate", DateTime.UtcNow.ToString("yyyy-MM-dd")),
            new XElement(cbc + "DocumentCurrencyCode", comprobante.Moneda.Value),
            new XElement(cac + "AccountingSupplierParty",
                new XElement(cac + "Party",
                    new XElement(cac + "PartyIdentification",
                        new XElement(cbc + "ID", new XAttribute("schemeID", "6"), comprobante.RucEmisor.Value)
                    )
                )
            ),
            new XElement(cac + "TaxTotal",
                new XElement(cbc + "TaxAmount", new XAttribute("currencyID", comprobante.Moneda.Value), igv)
            ),
            new XElement(cac + "LegalMonetaryTotal",
                new XElement(cbc + "LineExtensionAmount", new XAttribute("currencyID", comprobante.Moneda.Value), subTotal),
                new XElement(cbc + "TaxInclusiveAmount", new XAttribute("currencyID", comprobante.Moneda.Value), total),
                new XElement(cbc + "PayableAmount", new XAttribute("currencyID", comprobante.Moneda.Value), total)
            )
        );

        return invoice.ToString();
    }
}
