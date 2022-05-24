namespace SysVentas.Facturacion.Domain;
public class InvoiceDetail
{
    private InvoiceDetail()
    {
        
    }
    public InvoiceDetail(InvoiceMaster invoiceMaster, long productId, decimal quantity, decimal price)
    {
        InvoiceMaster = invoiceMaster;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
        Total = Quantity * price;
    }
    public long Id { get; set; }
    public InvoiceMaster InvoiceMaster { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public long InvoiceMasterId { get; set; }
}
