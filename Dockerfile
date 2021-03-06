﻿FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY sisVentas_facturacion_ms.sln ./
COPY SysVentas.Facturacion.Domain/*.csproj ./SysVentas.Facturacion.Domain/
COPY SysVentas.Facturacion.WebApi/*.csproj ./SysVentas.Facturacion.WebApi/
COPY SysVentas.Facturation.Application/*.csproj ./SysVentas.Facturation.Application/
COPY SysVentas.Facturation.Infrastructure.Data/*.csproj ./SysVentas.Facturation.Infrastructure.Data/
COPY SysVentas.Facturation.Infrastructure.HttpServices/*.csproj ./SysVentas.Facturation.Infrastructure.HttpServices/

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SysVentas.Facturacion.WebApi.dll"]
