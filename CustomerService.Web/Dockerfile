#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CustomerService.Web/CustomerService.Web.csproj", "CustomerService.Web/"]
COPY ["CustomerService.Application/CustomerService.Application.csproj", "CustomerService.Application/"]
COPY ["CustomerService.Domain/CustomerService.Domain.csproj", "CustomerService.Domain/"]
COPY ["Common.Domain/Common.Domain.csproj", "Common.Domain/"]
COPY ["Common.Configuration/Common.Configuration.csproj", "Common.Configuration/"]
COPY ["Common.Infrastructure/Common.Infrastructure.csproj", "Common.Infrastructure/"]
COPY ["CustomerService.Infrastructure/CustomerService.Infrastructure.csproj", "CustomerService.Infrastructure/"]
RUN dotnet restore "CustomerService.Web/CustomerService.Web.csproj"
COPY . .
WORKDIR "/src/CustomerService.Web"
RUN dotnet build "CustomerService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerService.Web.dll"]