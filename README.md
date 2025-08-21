# DesafioBackend

API REST para gerenciamento de motos, entregadores e locações

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

## Arquitetura
- Projeto estruturado em camadas:
  - Controllers: Camada de entrada da API (HTTP)
  - Model: Entidades e contexto do banco de dados
  - Business: Regras de negócio (interfaces e implementações)
  - Repository: Persistência de dados (interfaces e implementações)
  - Migrations: Controle de versões do banco
- API versioning habilitado
- Swagger para documentação interativa
- Injeção de dependência para serviços e repositórios
- Logging com Serilog

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
"DefaultConnection": "Host=db;Port=5432;Database=mottu;Username=mottuUser;Password=mottuPass;"
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

## Observações
- O arquivo `appsettings.json` contém as configurações de banco de dados.
- O projeto está organizado por domínio (Controllers, Model, Business, Repository).
- Para desenvolvimento, utilize o ambiente `Development` e configure o banco local.
- Para produção, utilize Docker e configure o banco conforme o ambiente.

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
	"modelo": "Mottu Sport",
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
		"modelo": "Mottu Sport",
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
	"modelo": "Mottu Sport",
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
## Considerações finais
- Foi incluido o suporte a versionamento de API.
  
## Proximos Passos
- Habilitar CORS
- Melhorar logs
- Adicionar Autenticação
