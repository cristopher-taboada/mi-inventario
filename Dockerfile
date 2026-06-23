# Etapa 1: Construcción con .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo del proyecto y restaurar dependencias
COPY LOGIN.csproj .
RUN dotnet restore

# Copiar el resto del código
COPY . .

# ---- NUEVAS LÍNEAS PARA LIMPIAR ----
# Eliminar archivos de bloqueo corruptos
RUN rm -rf obj/
RUN rm -rf bin/
RUN rm -rf /root/.nuget/packages/*
# --------------------------------

# Construir y publicar la aplicación
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Ejecución con .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "LOGIN.dll"]