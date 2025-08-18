using Asp.Versioning;
using DesafioBackend.Model;
using DesafioBackend.Business;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackend.Controllers;

[ApiVersion("1")]
[ApiController]
[Route("api/[controller]/v{version:apiVersion}")]
public class MotosController : ControllerBase
{  
    private readonly ILogger<MotosController> _logger;
    private IMotoBusiness _motoBusiness;

    public MotosController(ILogger<MotosController> logger, IMotoBusiness motoBusiness)
    {
        _logger = logger;
        _motoBusiness = motoBusiness;
    }

    [HttpPost]
    public IActionResult CadastrarMoto([FromBody] Moto moto)
    {
        if (moto == null ||
            string.IsNullOrEmpty(moto.Identificador) ||
            string.IsNullOrEmpty(moto.Modelo) ||
            string.IsNullOrEmpty(moto.Placa) ||
            moto.Ano <= 0)
        {
            return BadRequest(new { mensagem = "Dados inválidos" });
        }

        try
        {
            var novaMoto = _motoBusiness.CadastrarMoto(moto);
            return Created("", novaMoto);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
    }

    // GET /motos?placa=CDX-0101
    [HttpGet]
    public IActionResult ConsultarMotos([FromQuery] string? placa)
    {

        IEnumerable<Moto> motos = _motoBusiness.ConsultarMotos(placa);

        if (!string.IsNullOrEmpty(placa) && !motos.Any())
            return NotFound(new { mensagem = "Placa não cadastrada!" });

        return Ok(motos);
    }

    [HttpGet("{id}")]
    public IActionResult ConsultarPorId(long id)
    {
        var moto = _motoBusiness.ConsultaPorId(id);
        if (moto == null)
            return NotFound(new { mensagem = "Moto não encontrada!" });

        return Ok(moto);
    }
    
    [HttpPut("{id}")]
    public IActionResult ModificarPlaca(long id, [FromBody]  string placa)
    {
        if (id <= 0)
            return BadRequest(new { mensagem = "Dados inválidos" });
        

        if (string.IsNullOrEmpty(placa))
            return BadRequest(new { mensagem = "Dados inválidos" });


        try
        {
            _motoBusiness.ModificarPlaca(id, placa);
            return Ok(new { mensagem = "Placa modificada com sucesso" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
        catch (Exception)
        {
            // erro inesperado, evita expor detalhes para o cliente
            return StatusCode(500, new { mensagem = "Ocorreu um erro interno no servidor." });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult RemoverMoto(long id)
    {       
        _motoBusiness.DeletarMoto(id);

        return Ok();
    }
}

