# MSConsumers

## Descrição
Microserviço responsável por gerenciar consumidores e seus endereços.

## Tecnologias Utilizadas
- .NET 9.0
- Entity Framework Core
- PostgreSQL
- MediatR
- FluentValidation
- AutoMapper
- Swagger/OpenAPI

## Estrutura do Projeto
O projeto segue a arquitetura limpa (Clean Architecture) e os princípios do Domain-Driven Design (DDD).

```
MSConsumers/
├── MSConsumers.Api/           # Camada de apresentação (API)
├── MSConsumers.Application/   # Camada de aplicação (casos de uso)
├── MSConsumers.Domain/        # Camada de domínio (entidades e regras de negócio)
└── MSConsumers.Infrastructure/# Camada de infraestrutura (repositórios, serviços externos)
```

## Endpoints

### Consumidores

#### Criar Consumidor
```http
POST /api/consumers
```

#### Obter Consumidor por ID
```http
GET /api/consumers/{id}
```

#### Atualizar Consumidor
```http
PUT /api/consumers
```

#### Deletar Consumidor
```http
DELETE /api/consumers/{id}
```

### Endereços

#### Criar Endereço
```http
POST /api/addresses
```

#### Obter Endereço por ID
```http
GET /api/addresses/{id}
```

#### Obter Endereços por Consumidor
```http
GET /api/addresses/consumer/{consumerId}
```

#### Atualizar Endereço
```http
PUT /api/addresses
```

#### Deletar Endereço
```http
DELETE /api/addresses/{id}
```

## Configuração do Banco de Dados
O projeto utiliza PostgreSQL como banco de dados. As configurações de conexão devem ser definidas no arquivo `appsettings.json`.

## Executando o Projeto
1. Clone o repositório
2. Restaure as dependências:
   ```bash
   dotnet restore
   ```
3. Execute as migrações do banco de dados:
   ```bash
   dotnet ef database update
   ```
4. Inicie o projeto:
   ```bash
   dotnet run --project MSConsumers.Api
   ```

## Documentação da API
A documentação da API está disponível através do Swagger UI em:
```
http://localhost:5163/swagger
```

## Licença
Este é um projeto privado e não aceita contribuições públicas. 