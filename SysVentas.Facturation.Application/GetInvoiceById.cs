using FluentValidation;
using MediatR;
using SysVentas.Facturacion.Domain;
using SysVentas.Facturacion.Domain.Contracts;
namespace SysVentas.Facturation.Application;

public class GetInvoiceById : IRequestHandler<GetInvoiceById.Request, GetInvoiceById.InvoiceMasterByIdModelView>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetInvoiceById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public Task<InvoiceMasterByIdModelView> Handle(Request request, CancellationToken cancellationToken)
    {
        return Task.FromResult((MapMaster(LoadInvoices(request.InvoiceId))));
    }
    private InvoiceMaster? LoadInvoices(long invoiceId)
    {
        return _unitOfWork.GenericRepository<InvoiceMaster>().FindFirstOrDefault(t=> t.Id == invoiceId, includeProperties: "Details");
    }
    private static InvoiceMasterByIdModelView MapMaster(InvoiceMaster? t)
    {
        return new InvoiceMasterByIdModelView(t.Id, t.ClientId, t.Date, t.StatusView, t.Total, t.Details.Select(MapDetail));
    }
    private static InvoiceDetailModelView MapDetail(InvoiceDetail d)
    {
        return new InvoiceDetailModelView(d.ProductId, d.Quantity, d.Price, d.Total);
    }
    public record Request(long InvoiceId) : IRequest<InvoiceMasterByIdModelView>;
    public record InvoiceMasterByIdModelView(long Id, long ClientId, DateTime Date, string Status, decimal Total, IEnumerable<InvoiceDetailModelView> Details);
    public record InvoiceDetailModelView(long ProductId, decimal Quantity, decimal Price, decimal Total);
    public class Validation : AbstractValidator<Request>
    {
        private readonly IUnitOfWork _unitOfWork;
        public Validation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(t => t.InvoiceId).Must(ExistInvoice).WithMessage("Invoice not found");
        }
        private bool ExistInvoice(long invoiceId)
        {
            return _unitOfWork.GenericRepository<InvoiceMaster>().Find(invoiceId) != null;
        }
    }
}
