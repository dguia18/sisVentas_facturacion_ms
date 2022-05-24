namespace SysVentas.Facturacion.Domain;

public class InvoiceMaster
{
    public long Id { get; set; }
    public long ClientId { get; set; }
    public DateTime Date { get; set; }
    public InvoiceMasterStatus Status { get; set; }
    public IEnumerable<InvoiceDetail> Details { get; set; }
}
public enum InvoiceMasterStatus
{
    Cancel,
    Approved
}
