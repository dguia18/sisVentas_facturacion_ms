using MediatR;
using SysVentas.Facturacion.Domain;
using SysVentas.Facturacion.Domain.Contracts;
namespace SysVentas.Facturation.Application;

public class GetInvoices : IRequestHandler<GetInvoices.Request, GetInvoices.Response>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetInvoices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new Response(LoadInvoices().Select(MapMaster)));
    }
    private IEnumerable<InvoiceMaster> LoadInvoices()
    {
        return _unitOfWork.GenericRepository<InvoiceMaster>().FindBy();
    }
    private static InvoiceMasterModelView MapMaster(InvoiceMaster t)
    {
        return new InvoiceMasterModelView(t.Id, t.ClientId, t.Date, t.StatusView, t.Total);
    }
    public record Request : IRequest<Response>;
    public record Response(IEnumerable<InvoiceMasterModelView> Invoices);
    public record InvoiceMasterModelView(long Id, long ClientId, DateTime Date, string Status, decimal Total);
}
