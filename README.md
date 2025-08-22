# DesafioBackend

API REST para gerenciamento de motos, entregadores e locações.

## Principais Features

- **Tratamento de erros**: Respostas padronizadas para erros de validação, dados inválidos e exceções internas.
- **Arquitetura em camadas**: Separação clara entre Controllers, Business, Repository e Model, facilitando manutenção e testes.
- **Código em inglês**: Todo o código-fonte e nomenclatura seguem padrão internacional.
- **Suporte a versionamento de API**: Implementado conforme boas práticas, permitindo evolução sem quebra de contratos.
- **Utilização do Entity Framework Core**: ORM para persistência, migrations e relacionamento entre entidades.
- **Suporte a Docker e Docker Compose**: Deploy facilitado e ambiente isolado para desenvolvimento e produção.
- **Documentação interativa via Swagger**: Exploração dos endpoints diretamente pelo navegador.
- **Migrations automáticas com Evolve**: Controle de versões do banco de dados.
- **Configuração flexível via appsettings.json**: Facilita adaptação para diferentes ambientes.
- **Pronto para integração com autenticação e CORS**: Estrutura preparada para futuras melhorias de segurança.
- **Logging estruturado com Serilog**: Monitoramento e rastreabilidade das operações. [Em andamento]
- **Mensageria com RabbitMQ**: Notificação e persistência de cadastro de motos do ano de 2024.

## Tecnologias Utilizadas
- .NET 8.0
- ASP.NET Core
- Entity Framework Core 9.0.4
- Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4
- Serilog (logging)
- Swashbuckle.AspNetCore (Swagger)
- Evolve (migrations)
- Docker e Docker Compose
- PostgreSQL
- RabbitMQ (mensageria)

## Arquitetura
- Projeto estruturado em camadas:
  - Controllers: Camada de entrada da API (HTTP)
  - Model: Entidades e contexto do banco de dados
  - Business: Regras de negócio (interfaces e implementações)
  - Repository: Persistência de dados (interfaces e implementações)
  - Migrations: Controle de versões do banco
  - Mensageria: Integração com RabbitMQ para eventos
- API versioning habilitado
- Swagger para documentação interativa
- Injeção de dependência para serviços e repositórios
- Logging com Serilog
- RabbitMQ para eventos de cadastro de motos 2024

## Docker
O projeto pode ser executado em containers Docker. O arquivo `docker-compose.yml` define os serviços:
- **desafio-backend**: API .NET rodando na porta 8080 (exposta como 44300)
- (Opcional) Banco de dados PostgreSQL pode ser configurado como serviço no compose

### Build e execução com Docker Compose
```powershell
docker-compose up -d --build
```
A aplicação estará disponível em `https://localhost:44300/swagger`.

Para conectar ao banco de dados dentro do container, ajuste a string de conexão em `appsettings.json`:
```json
"DefaultConnection": "Host=db;Port=5432;Database=NOMEBANCO;Username=USUARIO;Password=SENHA;"
```

## Mensageria com RabbitMQ

Quando uma moto do ano de **2024** é cadastrada, o sistema publica uma mensagem no RabbitMQ. Um consumidor escuta essa fila e salva a notificação no banco de dados.

- **Producer:** Ao cadastrar uma moto do ano 2024, uma mensagem é enviada para a fila `moto_cadastrada`.
- **Consumer:** Um serviço consome essa fila e registra a notificação no banco, permitindo rastreabilidade e integração com outros sistemas.

### Exemplo de fluxo

1. **POST** `/motos` com body:
    ```json
    {
        "identificador": "moto2024",
        "ano": 2024,
        "modelo": "Nova Moto",
        "placa": "XYZ-2024"
    }
    ```
2. O backend verifica o ano e publica uma mensagem no RabbitMQ.
3. O consumidor recebe a mensagem e salva uma notificação no banco.

<!-- #### Configuração RabbitMQ no Docker Compose

```yaml
services:
  rabbitmq:
    image: rabbitmq:3-management
    ports:
      - "5672:5672"
      - "15672:15672"
``` -->
Para visualizar via RabbitMQ Management Web UI

1. Certifique-se de que o Management Plugin está habilitado:
```bash

   rabbitmq-plugins enable rabbitmq_management

```
2. Acesse pelo navegador:

```bash

   http://localhost:15672

```
- Usuário padrão: guest
- Senha padrão: guest
 


#### Exemplo de mensagem publicada

```json
{
    "identificador": "moto2024",
    "ano": 2024,
    "modelo": "Nova Moto",
    "placa": "XYZ-2024",
    "dataCadastro": "2024-06-01T10:00:00"
}
```

## Como executar localmente
1. Instale o .NET 8.0
2. Configure a string de conexão do PostgreSQL em `appsettings.json` e/ou `appsettings.Development.json`
3. Execute as migrations (se necessário):
	```powershell
	dotnet ef database update
	```
	ou no Package Manage Console
	```
	Update-Database
	```
4. Execute o projeto:
	```powershell
	dotnet run --project DesafioBackend/DesafioBackend.csproj
	```
5. Acesse os endpoints via Postman, Insomnia ou navegador

## Estrutura do Projeto

```
DesafioBackend/
├── Controllers/
│   ├── DeliveryDriversController.cs
│   ├── MotorcyclesController.cs
│   └── RentalsController.cs
├── Model/
│   ├── DeliveryDriver.cs
│   ├── Motorcycle.cs
│   ├── Rental.cs
│   └── Context/
│       └── AppDbContext.cs
├── Business/
│   ├── IDeliveryDriverBusiness.cs
│   ├── IMotorcycleBusiness.cs
│   ├── IRentalBusiness.cs
│   └── Implementations/
│       ├── DeliveryDriverBusinessImplementation.cs
│       ├── MotorcycleBusinessImplementation.cs
│       └── RentalBusinessImplementation.cs
├── Repository/
│   ├── IDeliveryDriverRepository.cs
│   ├── IMotorcycleRepository.cs
│   ├── IRentalRepository.cs
│   └── Implementations/
│       ├── DeliveryDriverRepositoryImplementation.cs
│       ├── MotorcycleRepositoryImplementation.cs
│       └── RentalRepositoryImplementation.cs
├── Migrations/
│   └── <arquivos de migração do EF Core>
├── Messaging/
│   ├── MotorcycleRegisteredConsumer.cs
│   └── MotorcycleRegisteredPublisher.cs
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── Dockerfile
├── DesafioBackend.csproj
├── DesafioBackend.sln
└── ...
```

## Endpoints Moto

### Cadastrar uma nova moto
**POST** `/motos`
Body:
```json
{
	"identificador": "moto123",
	"ano": 2020,
	"modelo": "moto Sport",
	"placa": "CDX-0101"
}
```
Response:
**201 Created**
```json
{
	"mensagem": "Moto cadastrada com sucesso"
}
```
**400 Bad Request**
```json
{
	"mensagem": "Dados inválidos"
}
```

### Consultar motos existentes
**GET** `/motos?placa=CDX-0101`
Response:
**200 OK**
```json
[
	{
		"identificador": "moto123",
		"ano": 2020,
		"modelo": "moto Sport",
		"placa": "CDX-0101"
	}
]
```

### Consultar moto por ID
**GET** `/motos/{id}`
Response:
**200 OK**
```json
{
	"identificador": "moto123",
	"ano": 2020,
	"modelo": "moto Sport",
	"placa": "CDX-0101"
}
```
**404 Not Found**
```json
{
	"mensagem": "Moto não encontrada"
}
```
**400 Bad Request**
```json
{
	"mensagem": "Request mal formada"
}
```

### Modificar a placa de uma moto
**PUT** `/motos/{id}/placa`
Body:
```json
{
	"placa": "ABC-1234"
}
```
Response:
**200 OK**
```json
{
	"mensagem": "Placa modificada com sucesso"
}
```
**400 Bad Request**
```json
{
	"mensagem": "Dados inválidos"
}
```

### Remover uma moto
**DELETE** `/motos/{id}`
Response:
**200 OK**
```json
{
	"mensagem": "Moto removida com sucesso"
}
```
**400 Bad Request**
```json
{
	"mensagem": "Dados inválidos"
}
```


## Endpoints Entregador

### Cadastrar um novo entregador

**POST** `/entregadores`

**Body:**
```json
{
  "identificador": "entregador123",
  "nome": "João da Silva",
  "cnpj": "12345678901234",
  "data_nascimento": "1990-01-01T00:00:00Z",
  "numero_cnh": "12345678900",
  "tipo_cnh": "A",
  "imagem_cnh": "base64string"
}
```

**Respostas:**
- **201 Created**
	```json
	{
		"mensagem": "Entregador cadastrado com sucesso"
	}
	```
- **400 Bad Request**
	```json
	{
		"mensagem": "Dados inválidos"
	}
	```
- **500 Internal Server Error**
	```json
	{
		"mensagem": "Erro interno ao cadastrar entregador"
	}
	```

---

### Enviar imagem da CNH do entregador

**POST** `/entregadores/{id}/cnh`

**Body:**
```json
{
  "imagem_cnh": "base64string"
}
```

**Respostas:**
- **201 Created**
	```json
	{
		"mensagem": "Foto da CNH enviada com sucesso"
	}
	```
- **400 Bad Request**
	```json
	{
		"mensagem": "A imagem da CNH deve estar em base64."
	}
	```

---

## Endpoints Locação

### Cadastrar uma nova locação

**POST** `/locacao`

**Body:**
```json
{
	"idMoto": 1,
	"idEntregador": 2,
	"dataInicio": "2025-08-21T10:00:00",
	"dataFimPrevista": "2025-09-21T10:00:00"
}
```

**Respostas:**
- **201 Created**
	```json
	{
		"id": 1,
		"idMoto": 1,
		"idEntregador": 2,
		"dataInicio": "2025-08-21T10:00:00",
		"dataFimPrevista": "2025-09-21T10:00:00",
		"dataFimReal": null
	}
	```
- **400 Bad Request**
	```json
	{
		"mensagem": "Dados inválidos"
	}
	```

---

### Consultar locação por ID

**GET** `/locacao/{id}`

**Respostas:**
- **200 OK**
	```json
	{
		"id": 1,
		"idMoto": 1,
		"idEntregador": 2,
		"dataInicio": "2025-08-21T10:00:00",
		"dataFimPrevista": "2025-09-21T10:00:00",
		"dataFimReal": null
	}
	```
- **404 Not Found**
	```json
	{
		"mensagem": "Locação não encontrada"
	}
	```

---

### Registrar devolução de uma locação

**PUT** `/locacao/{id}/devolucao`

**Body:**
```json
"2025-09-20T15:30:00"
```

**Respostas:**
- **200 OK**
	```json
	{
		"mensagem": "Data de devolução informada com sucesso"
	}
	```
- **400 Bad Request**
	```json
	{
		"mensagem": "Dados inválidos"
	}
	```
## Models
### Motorcycle
```csharp
public class Motorcycle {
	public long Id { get; set; }
	public string Identificador { get; set; }
	public int Ano { get; set; }
	public string Modelo { get; set; }
	public string Placa { get; set; }
}
```

### DeliveryDriver
```csharp
public class DeliveryDriver {
	public long Id { get; set; }
	public string Identificador { get; set; }
	public string Nome { get; set; }
	public string Cnpj { get; set; }
	public DateTime DataNascimento { get; set; }
	public string NumeroCnh { get; set; }
	public string TipoCnh { get; set; }
	public string ImagemCnh { get; set; }
}
```

### Rental
```csharp
public class Rental {
	public long Id { get; set; }
	public string Identificador { get; set; }
	public decimal ValorDiaria { get; set; }
	public long DeliveryDriverId { get; set; }
	public DeliveryDriver DeliveryDriver { get; set; }
	public long MotorcycleId { get; set; }
	public Motorcycle Motorcycle { get; set; }
	public DateTime DataInicio { get; set; }
	public DateTime DataTermino { get; set; }
	public DateTime DataPrevisaoTermino { get; set; }
	public DateTime? DataDevolucao { get; set; }
	public int Plano { get; set; }
}
```
## Próximos Passos
- Habilitar CORS
- Melhorar logs
- Adicionar autenticação
