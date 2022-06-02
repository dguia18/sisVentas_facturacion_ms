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
    [HttpPut("{id:long}/canceled")]
    public async Task<IActionResult> Cancel(long id)
    {
        var response = await _mediator.Send(new CancelInvoice.Request(id));
        return  Ok(response);
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _mediator.Send(new GetInvoices.Request());
        return  Ok(response);
    }
    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var response = await _mediator.Send(new GetInvoiceById.Request(id));
        return  Ok(response);
    }
}
