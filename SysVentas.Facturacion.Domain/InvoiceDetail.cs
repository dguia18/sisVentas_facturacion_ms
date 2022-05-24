namespace SysVentas.Facturacion.Domain;
public class InvoiceDetail
{
    public long Id { get; set; }
    public InvoiceMaster InvoiceMaster { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public long InvoiceMasterId { get; set; }
}
