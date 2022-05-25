using System.Reflection;
using Infrastructure.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SysVentas.Facturacion.Domain.Contracts;
using SysVentas.Facturacion.Domain.Services;
using SysVentas.Facturation.Infrastructure.Data;
using SysVentas.Facturation.Infrastructure.Data.Base;
using SysVentas.Facturation.Infrastructure.HttpServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FacturationDataContext>(options => options.UseInMemoryDatabase("Facturation"));
builder.Services.AddScoped<IDbContext, FacturationDataContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IProductService,ProductService>();
builder.Services.AddMediatR(Assembly.Load("SysVentas.Facturation.Application"));
builder.Services.Configure<IProductService.ApisUrl>(
    opts => builder.Configuration.GetSection("ApiUrls").Bind(opts)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
