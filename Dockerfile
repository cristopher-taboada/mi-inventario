# Etapa 1: Construcción con .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar el archivo del proyecto y restaurar dependencias
COPY LOGIN.csproj .
RUN dotnet restore

# Copiar el resto del código y construir
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Ejecución con .NET 10
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto
EXPOSE 80

# Iniciar la aplicación
ENTRYPOINT ["dotnet", "LOGIN.dll"]