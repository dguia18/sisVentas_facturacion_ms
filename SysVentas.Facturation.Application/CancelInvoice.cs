using MediatR;
using SysVentas.Facturacion.Domain;
using SysVentas.Facturacion.Domain.Contracts;
using SysVentas.Facturacion.Domain.Services;
using SysVentas.Facturation.Application.Base;
namespace SysVentas.Facturation.Application;

public class CancelInvoice : IRequestHandler<CancelInvoice.Request, CancelInvoice.Response>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductService _productService;
    public CancelInvoice(IUnitOfWork unitOfWork, IProductService productService)
    {
        _unitOfWork = unitOfWork;
        _productService = productService;
    }
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var invoice = LoadInvoice(request.InvoiceId);
        if (invoice == null) throw new SysVentasApplicationException(Messages.CancelInvoice_Handle_Invoice_not_found);
        invoice.Cancel();
        
        await _productService
            .UpdateStock(new UpdateStockRequest(invoice.Details.Select(t => new Items(t.ProductId, t.Quantity))));
        
        return new Response(Messages.CancelInvoice_Handle_Invoice_cancelled_successfully);
    }
    private InvoiceMaster? LoadInvoice(long invoiceId)
    {
        return _unitOfWork
            .GenericRepository<InvoiceMaster>()
            .FindFirstOrDefault(t => t.Id == invoiceId, includeProperties: "Details");
    }
    public record Request(long InvoiceId) : IRequest<Response>;
    public record Response(string Mensaje);
}
