using MediatR;
using SysVentas.Facturacion.Domain;
using SysVentas.Facturacion.Domain.Contracts;
namespace SysVentas.Facturation.Application;

public class GetInvoices : IRequestHandler<GetInvoices.Request, IEnumerable<GetInvoices.InvoiceMasterModelView>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetInvoices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public Task<IEnumerable<InvoiceMasterModelView>> Handle(Request request, CancellationToken cancellationToken)
    {
        return Task.FromResult((LoadInvoices().Select(MapMaster)));
    }
    private IEnumerable<InvoiceMaster> LoadInvoices()
    {
        return _unitOfWork.GenericRepository<InvoiceMaster>().FindBy();
    }
    private static InvoiceMasterModelView MapMaster(InvoiceMaster t)
    {
        return new InvoiceMasterModelView(t.Id, t.ClientId, t.Date, t.StatusView, t.Total);
    }
    public record Request : IRequest<IEnumerable<InvoiceMasterModelView>>;
    public record InvoiceMasterModelView(long Id, long ClientId, DateTime Date, string Status, decimal Total);
}
