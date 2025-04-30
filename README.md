# MsConsumers - Microserviço de Consumidores

Microserviço desenvolvido em ASP.NET Core 9.0 para gerenciamento de consumidores, seguindo os princípios de Clean Architecture, Domain-Driven Design (DDD) e CQRS.

## 🏗️ Arquitetura

O projeto segue a Clean Architecture com as seguintes camadas:

- **MsConsumers.Api**: Camada de apresentação, contendo os controllers e configurações da API
- **MsConsumers.Application**: Camada de aplicação, contendo os casos de uso, commands, handlers e interfaces
- **MsConsumers.Domain**: Camada de domínio, contendo as entidades e regras de negócio
- **MsConsumers.Infrastructure**: Camada de infraestrutura, contendo implementações de repositórios, serviços externos e configurações

## 🛠️ Tecnologias Utilizadas

- .NET 9.0
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Swagger/OpenAPI
- Clean Architecture
- DDD (Domain-Driven Design)
- CQRS (Command Query Responsibility Segregation)
- SOLID Principles
- MediatR

## 📋 Pré-requisitos

- .NET 9.0 SDK
- PostgreSQL 15 ou superior
- Visual Studio 2022 ou VS Code

## ⚙️ Configuração do Ambiente

1. Clone o repositório:
```bash
git clone [url-do-repositorio]
```

2. Configure o banco de dados no arquivo `MsConsumers.Api/appsettings.Development.json`:
```json
{
  "DatabaseSettings": {
    "Host": "localhost",
    "Port": "5432",
    "Database": "consumer",
    "Username": "postgres",
    "Password": "your_password"
  }
}
```

3. Restaure os pacotes NuGet:
```bash
dotnet restore
```

4. Execute as migrations:
```bash
dotnet ef database update --project MsConsumers.Infrastructure --startup-project MsConsumers.Api
```

## 🚀 Executando o Projeto

1. Execute o projeto:
```bash
dotnet run --project MsConsumers.Api
```

2. Acesse a documentação Swagger em:
```
https://localhost:5001/swagger
```

## 📚 Estrutura do Banco de Dados

O banco de dados está organizado no schema `shc_consumer` com as seguintes tabelas:

### Tabelas

- `tb_consumers`: Armazena informações dos consumidores
  - Campos únicos: document_id, email, phone_number
  - Chaves estrangeiras: currency_id, phone_country_code_id, preferred_language_id, timezone_id

- `tb_consumer_address`: Armazena endereços dos consumidores
  - Chaves estrangeiras: consumer_id, country_id

- `tb_country_codes`: Armazena códigos de países
  - Campo único: code

- `tb_currencies`: Armazena moedas
  - Campo único: code

- `tb_languages`: Armazena idiomas
  - Campo único: code

- `tb_time_zones`: Armazena fusos horários
  - Campo único: name

### Índices

- `IX_tb_consumers_currency_id`
- `IX_tb_consumers_phone_country_code_id`
- `IX_tb_consumers_preferred_language_id`
- `IX_tb_consumers_timezone_id`
- `IX_tb_consumer_address_consumer_id`
- `IX_tb_consumer_address_country_id`

### Restrições Únicas

- `IX_tb_consumers_document_id`
- `IX_tb_consumers_email`
- `IX_tb_consumers_phone_number`
- `IX_tb_country_codes_code`
- `IX_tb_currencies_code`
- `IX_tb_languages_code`
- `IX_tb_time_zones_name`

## 🔐 Segurança

- As credenciais do banco de dados são armazenadas no arquivo de configuração
- Em produção, utilize variáveis de ambiente ou um gerenciador de segredos
- Implementação de autenticação e autorização (a ser implementada)

## 📝 Padrões de Código

- Nomes de arquivos e pastas em minúsculo, separados por underscore
- Classes e métodos em PascalCase
- Propriedades privadas com underscore prefix
- Documentação XML em classes e métodos públicos
- Entidades imutáveis após criação
- Validações no domínio
- Commands e Handlers para operações de escrita
- Queries para operações de leitura

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença [MIT](LICENSE).

## 📞 Suporte

Para suporte, entre em contato com a equipe de desenvolvimento. 