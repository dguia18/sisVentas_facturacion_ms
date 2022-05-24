using SysVentas.Facturacion.Domain.Base;
namespace SysVentas.Facturacion.Domain;

public class InvoiceMaster : Entity<long>
{
    public long ClientId { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public InvoiceMasterStatus Status { get; set; }
    public string StatusView => InvoiceMasterStatusView.Get(Status);
    public ICollection<InvoiceDetail> Details { get; set; }
    private InvoiceMaster()
    {
        
    }
    public InvoiceMaster(DateTime date, long clientId)
    {
        Date = date;
        ClientId = clientId;
        Status = InvoiceMasterStatus.Approved;
        Details = new List<InvoiceDetail>();
    }
    public void AddDetail(long productId, decimal quantity, decimal price)
    {
        var detail = new InvoiceDetail(this, productId, quantity, price);
        Total += detail.Total;
        Details.Add(detail);
    }
}
public enum InvoiceMasterStatus
{
    Canceled,
    Approved
}
public static class InvoiceMasterStatusView
{
    private static readonly IDictionary<InvoiceMasterStatus, string> Values = new Dictionary<InvoiceMasterStatus, string>()
    {
        {
            InvoiceMasterStatus.Approved, "Approved"
        },
        {
            InvoiceMasterStatus.Canceled, "Canceled"
        },
    };
    public static string Get(InvoiceMasterStatus status) => Values[status];
}
