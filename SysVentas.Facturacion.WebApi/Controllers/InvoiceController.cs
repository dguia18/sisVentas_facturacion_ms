using MediatR;
using Microsoft.AspNetCore.Mvc;
using SysVentas.Facturation.Application;
namespace SysVentas.Facturacion.WebApi.Controllers;

[Route("api/invoice")]
[ApiController]
public class InvoiceController : Controller
{
    private readonly IMediator _mediator;
    public InvoiceController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Post(CreateInvoice.Request request)
    {
        var response = await _mediator.Send(request);
        return  Ok(response);
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetInvoices.Request());
        return  Ok(response);
    }
}
