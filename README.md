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
**POST** `/api/locacao`
Body:
```json
{
	"identificador": "string",
	"valor_diaria": 100.0,
	"entregador_id": 1,
	"moto_id": 1,
	"data_inicio": "2025-08-20T08:00:00",
	"data_termino": "2025-08-25T08:00:00",
	"data_previsao_termino": "2025-08-25T08:00:00",
	"plano": 1
}
```
Response:
**201 Created**
```json
{
	"id": 1,
	"identificador": "string",
	"valor_diaria": 100.0,
	"entregador_id": 1,
	"moto_id": 1,
	"data_inicio": "2025-08-20T08:00:00",
	"data_termino": "2025-08-25T08:00:00",
	"data_previsao_termino": "2025-08-25T08:00:00",
	"data_devolucao": null,
	"plano": 1
}
```

### Listar Locacoes
**GET** `/api/locacao`
Response:
**200 OK**
```json
[
	{
		"id": 1,
		"identificador": "string",
		"valor_diaria": 100.0,
		"entregador_id": 1,
		"moto_id": 1,
		"data_inicio": "2025-08-20T08:00:00",
		"data_termino": "2025-08-25T08:00:00",
		"data_previsao_termino": "2025-08-25T08:00:00",
		"data_devolucao": null,
		"plano": 1
	}
]
```

### Consultar Locacao por ID
**GET** `/api/locacao/{id}`
Response:
**200 OK**
```json
{
	"id": 1,
	"identificador": "string",
	"valor_diaria": 100.0,
	"entregador_id": 1,
	"moto_id": 1,
	"data_inicio": "2025-08-20T08:00:00",
	"data_termino": "2025-08-25T08:00:00",
	"data_previsao_termino": "2025-08-25T08:00:00",
	"data_devolucao": null,
	"plano": 1
}
```
**404 Not Found**

### Remover Locacao
**DELETE** `/api/locacao/{id}`
Response:
**200 OK**
```json
{
	"mensagem": "Locacao removida com sucesso"
}
```
**404 Not Found**


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