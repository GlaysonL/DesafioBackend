
# DesafioBackend

API REST para gerenciamento de motos

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
Retorno: 201 Created, objeto Moto criado

### Consultar Motos
`GET /api/motos/v1?placa=ABC-1234`
Retorno: 200 OK, lista de motos

### Consultar Moto por ID
`GET /api/motos/v1/{id}`
Retorno: 200 OK, objeto Moto

### Modificar Placa
`PUT /api/motos/v1/{id}`
Body (string):
```
"XYZ-9876"
```
Retorno: 200 OK, mensagem de sucesso

### Remover Moto
`DELETE /api/motos/v1/{id}`
Retorno: 200 OK, mensagem de sucesso

## Modelo Moto
```csharp
public class Moto {
	public long Id { get; set; }
	public string Identificador { get; set; }
	public int Ano { get; set; }
	public string Modelo { get; set; }
	public string Placa { get; set; }
}
```

## Como executar
1. Instale o .NET 8.0
2. Execute `dotnet run` na pasta do projeto
3. Acesse os endpoints via Postman ou similar