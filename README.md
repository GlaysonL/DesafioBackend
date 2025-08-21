## Endpoints Entregador

### Criar Entregador
`POST /api/entregadores/v1`
Body (JSON):
```json
{
	"identificador": "string",
	"nome": "string",
	"cnpj": "12345678000199",
	"dataNascimento": "1990-01-01T00:00:00",
	"numeroCnh": "1234567890",
	"tipoCnh": "A",
	"imagemCnh": "base64string"
}
```
Resposta:
**201 Created**
```json
{
	"id": 1,
	"identificador": "string",
	"nome": "string",
	"cnpj": "12345678000199",
	"dataNascimento": "1990-01-01T00:00:00",
	"numeroCnh": "1234567890",
	"tipoCnh": "A",
	"imagemCnh": "base64string"
}
```

### Consultar Entregadores
`GET /api/entregadores/v1`
Resposta:
**200 OK**
```json
[
	{
		"id": 1,
		"identificador": "string",
		"nome": "string",
		"cnpj": "12345678000199",
		"dataNascimento": "1990-01-01T00:00:00",
		"numeroCnh": "1234567890",
		"tipoCnh": "A",
		"imagemCnh": "base64string"
	}
]
```

### Consultar Entregador por ID
`GET /api/entregadores/v1/{id}`
Resposta:
**200 OK**
```json
{
	"id": 1,
	"identificador": "string",
	"nome": "string",
	"cnpj": "12345678000199",
	"dataNascimento": "1990-01-01T00:00:00",
	"numeroCnh": "1234567890",
	"tipoCnh": "A",
	"imagemCnh": "base64string"
}
```
**404 Not Found** (se não existir)

### Remover Entregador
`DELETE /api/entregadores/v1/{id}`
Resposta:
**200 OK**
```json
{
	"mensagem": "Entregador removido com sucesso"
}
```
**404 Not Found** (se não existir)

## Endpoints Locacao

### Criar Locacao
`POST /api/locacoes/v1`
Body (JSON):
```json
{
	"identificador": "string",
	"valorDiaria": 100.0,
	"entregadorId": 1,
	"motoId": 1,
	"dataInicio": "2025-08-20T08:00:00",
	"dataTermino": "2025-08-25T08:00:00",
	"dataPrevisaoTermino": "2025-08-25T08:00:00",
	"plano": 1
}
```
Resposta:
**201 Created**
```json
{
	"id": 1,
	"identificador": "string",
	"valorDiaria": 100.0,
	"entregadorId": 1,
	"motoId": 1,
	"dataInicio": "2025-08-20T08:00:00",
	"dataTermino": "2025-08-25T08:00:00",
	"dataPrevisaoTermino": "2025-08-25T08:00:00",
	"dataDevolucao": null,
	"plano": 1
}
```

### Consultar Locacoes
`GET /api/locacoes/v1`
Resposta:
**200 OK**
```json
[
	{
		"id": 1,
		"identificador": "string",
		"valorDiaria": 100.0,
		"entregadorId": 1,
		"motoId": 1,
		"dataInicio": "2025-08-20T08:00:00",
		"dataTermino": "2025-08-25T08:00:00",
		"dataPrevisaoTermino": "2025-08-25T08:00:00",
		"dataDevolucao": null,
		"plano": 1
	}
]
```

### Consultar Locacao por ID
`GET /api/locacoes/v1/{id}`
Resposta:
**200 OK**
```json
{
	"id": 1,
	"identificador": "string",
	"valorDiaria": 100.0,
	"entregadorId": 1,
	"motoId": 1,
	"dataInicio": "2025-08-20T08:00:00",
	"dataTermino": "2025-08-25T08:00:00",
	"dataPrevisaoTermino": "2025-08-25T08:00:00",
	"dataDevolucao": null,
	"plano": 1
}
```
**404 Not Found** (se não existir)

### Remover Locacao
`DELETE /api/locacoes/v1/{id}`
Resposta:
**200 OK**
```json
{
	"mensagem": "Locacao removida com sucesso"
}
```
**404 Not Found** (se não existir)


# DesafioBackend

API REST para gerenciamento de motos

## Estrutura do Projeto

```
DesafioBackend/
├── Controllers/
│   └── MotosController.cs
├── Model/
│   ├── Moto.cs
│   └── Context/
│       └── AppDbContext.cs
├── Services/
│   ├── IMotoService.cs
│   └── Implementations/
│       └── MotoServiceImplementation.cs
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── DesafioBackend.csproj
├── DesafioBackend.sln
└── ...
```

## Tecnologias
- .NET 8.0
- Microsoft.EntityFrameworkCore 9.0.4
- Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4

## Endpoints

### Criar Moto
`POST /api/motos/v1`
Body (JSON):
```json
{
	"identificador": "string",
	"ano": 2020,
	"modelo": "string",
	"placa": "ABC-1234"
}
```
Resposta:
**201 Created**
```json
{
	"id": 1,
	"identificador": "string",
	"ano": 2020,
	"modelo": "string",
	"placa": "ABC-1234"
}
```

### Consultar Motos
`GET /api/motos/v1?placa=ABC-1234`
Resposta:
**200 OK**
```json
[
	{
		"id": 1,
		"identificador": "string",
		"ano": 2020,
		"modelo": "string",
		"placa": "ABC-1234"
	}
]
```

### Consultar Moto por ID
`GET /api/motos/v1/{id}`
Resposta:
**200 OK**
```json
{
	"id": 1,
	"identificador": "string",
	"ano": 2020,
	"modelo": "string",
	"placa": "ABC-1234"
}
```
**404 Not Found** (se não existir)

### Modificar Placa
`PUT /api/motos/v1/{id}`
Body (string):
```
"XYZ-9876"
```
Resposta:
**200 OK**
```json
{
	"mensagem": "Placa modificada com sucesso"
}
```
**404 Not Found** (se não existir)

### Remover Moto
`DELETE /api/motos/v1/{id}`
Resposta:
**200 OK**
```json
{
	"mensagem": "Moto removida com sucesso"
}
```
**404 Not Found** (se não existir)


## Models

### Moto
```csharp
public class Moto {
	public long Id { get; set; }
	public string Identificador { get; set; }
	public int Ano { get; set; }
	public string Modelo { get; set; }
	public string Placa { get; set; }
}
```

### Entregador
```csharp
public class Entregador {
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

### Locacao
```csharp
public class Locacao {
	public long Id { get; set; }
	public string Identificador { get; set; }
	public decimal ValorDiaria { get; set; }
	public long EntregadorId { get; set; }
	public Entregador Entregador { get; set; }
	public long MotoId { get; set; }
	public Moto Moto { get; set; }
	public DateTime DataInicio { get; set; }
	public DateTime DataTermino { get; set; }
	public DateTime DataPrevisaoTermino { get; set; }
	public DateTime? DataDevolucao { get; set; }
	public int Plano { get; set; }
}
```

## Como executar
1. Instale o .NET 8.0
2. Configure a string de conexão do PostgreSQL em `appsettings.json` e/ou `appsettings.Development.json`
3. Execute as migrations (se necessário):
	 ```powershell
	 dotnet ef database update
	 ```
4. Execute o projeto:
	 ```powershell
	 dotnet run --project DesafioBackend/DesafioBackend.csproj
	 ```
5. Acesse os endpoints via Postman, Insomnia ou navegador

## Observações
- O arquivo `appsettings.json` contém as configurações de banco de dados.
- O projeto está organizado por domínio (Controllers, Model, Services).