FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar os arquivos de projeto
COPY ["MSConsumers.Api/MSConsumers.Api.csproj", "MSConsumers.Api/"]
COPY ["MSConsumers.Application/MSConsumers.Application.csproj", "MSConsumers.Application/"]
COPY ["MSConsumers.Domain/MSConsumers.Domain.csproj", "MSConsumers.Domain/"]
COPY ["MSConsumers.Infrastructure/MSConsumers.Infrastructure.csproj", "MSConsumers.Infrastructure/"]

# Restaurar as dependências
RUN dotnet restore "MSConsumers.Api/MSConsumers.Api.csproj"

# Copiar o resto do código
COPY . .

# Publicar a aplicação
RUN dotnet publish "MSConsumers.Api/MSConsumers.Api.csproj" -c Release -o /app/publish

# Imagem final
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiar os arquivos publicados
COPY --from=build /app/publish .

# Copiar os arquivos de configuração
COPY ["MSConsumers.Api/appsettings.json", "appsettings.json"]
COPY ["MSConsumers.Api/appsettings.Production.json", "appsettings.Production.json"]

# Configurar variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80
ENTRYPOINT ["dotnet", "MSConsumers.Api.dll"] 