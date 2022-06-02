using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SysVentas.Facturacion.Domain.Contracts;
using SysVentas.Facturacion.Domain.Services;
using SysVentas.Facturacion.WebApi.Infrastructure;
using SysVentas.Facturation.Infrastructure.Data;
using SysVentas.Facturation.Infrastructure.Data.Base;
using SysVentas.Facturation.Infrastructure.HttpServices;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FacturationDataContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString"]));
builder.Services.AddScoped<IDbContext, FacturationDataContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddMediatR(Assembly.Load("SysVentas.Facturation.Application"));
builder.Services.Configure<IProductService.ApisUrl>(
    opts => builder.Configuration.GetSection("ApiUrls").Bind(opts)
);
builder.Services.AddTransient (typeof (IPipelineBehavior<,>), typeof (ValidatorPipelineBehavior<,>));;
AssemblyScanner.FindValidatorsInAssembly(Assembly.Load("SysVentas.Facturation.Application")).ForEach(pair =>
{
    builder.Services.Add(ServiceDescriptor.Scoped(pair.InterfaceType,pair.ValidatorType));
    builder.Services.Add(ServiceDescriptor.Scoped(pair.ValidatorType,pair.ValidatorType));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.CreateScope().ServiceProvider.GetRequiredService<FacturationDataContext>().Database.Migrate();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
