using MediatR;
namespace SysVentas.Facturation.Application;

public class CreateInvoice : IRequestHandler<CreateInvoice.Request,CreateInvoice.Response>
{
    public Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public record Request : IRequest<Response>
    {
        public long ClientId { get; set; }
        public InvoiceDetail Details { get; set; }
        public record InvoiceDetail
        {
            public long ProductId { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
    public record Response
    {
        public string Mensaje { get; set; }
    }
}
