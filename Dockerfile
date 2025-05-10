FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
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
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Configurar variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80
ENTRYPOINT ["dotnet", "MSConsumers.Api.dll"] 