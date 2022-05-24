using MediatR;
using SysVentas.Facturacion.Domain;
using SysVentas.Facturacion.Domain.Contracts;
namespace SysVentas.Facturation.Application;

public class CreateInvoice : IRequestHandler<CreateInvoice.Request,CreateInvoice.Response>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateInvoice(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var invoice = new InvoiceMaster(request.Date, request.ClientId);
        foreach (var detail in request.Details)
        {
            invoice.AddDetail(detail.ProductId,detail.Quantity,detail.Price);
        }
        _unitOfWork.GenericRepository<InvoiceMaster>().Add(invoice);
        await _unitOfWork.CommitAsync();
        return new Response("Factura realizada con éxito");
    }
    public record Request : IRequest<Response>
    {
        public long ClientId { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<InvoiceDetail> Details { get; set; }
        public record InvoiceDetail
        {
            public long ProductId { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
    public record Response(string Mensaje);
}
