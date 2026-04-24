FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["src/InvoiceBroker.Api/InvoiceBroker.Api.csproj", "src/InvoiceBroker.Api/"]
COPY ["src/InvoiceBroker.Application/InvoiceBroker.Application.csproj", "src/InvoiceBroker.Application/"]
COPY ["src/InvoiceBroker.Domain/InvoiceBroker.Domain.csproj", "src/InvoiceBroker.Domain/"]
COPY ["src/InvoiceBroker.Infrastructure/InvoiceBroker.Infrastructure.csproj", "src/InvoiceBroker.Infrastructure/"]
RUN dotnet restore "src/InvoiceBroker.Api/InvoiceBroker.Api.csproj"

# Copy everything else and build
COPY src/ src/
WORKDIR "/src/src/InvoiceBroker.Api"
RUN dotnet build "InvoiceBroker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvoiceBroker.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

ENTRYPOINT ["dotnet", "InvoiceBroker.Api.dll"]
