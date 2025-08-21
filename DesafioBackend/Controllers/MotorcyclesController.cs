using Asp.Versioning;
using DesafioBackend.Business;
using DesafioBackend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace DesafioBackend.Controllers;

[ApiVersion("1")]
[ApiController]
[Route("api/motos/")]
public class MotorcyclesController : ControllerBase
{  
    private readonly ILogger<MotorcyclesController> _logger;
    private IMotorcycleBusiness _motorcycleBusiness;

    public MotorcyclesController(ILogger<MotorcyclesController> logger, IMotorcycleBusiness motorcycleBusiness)
    {
        _logger = logger;
        _motorcycleBusiness = motorcycleBusiness;
    }

    [HttpPost]
    public IActionResult Register([FromBody] Motorcycle motorcycle)
    {
        if (motorcycle == null ||
            string.IsNullOrEmpty(motorcycle.Identifier) ||
            string.IsNullOrEmpty(motorcycle.Model) ||
            string.IsNullOrEmpty(motorcycle.Plate) ||
            motorcycle.Year <= 0)
        {
            return BadRequest(new { mensagem = "Dados inválidos" });
        }

        try
        {
            var newMotorcycle = _motorcycleBusiness.Register(motorcycle);
            _logger.LogInformation(
                "Moto criada com sucesso: {@Motorcycle}",
                new
                {
                    Id = newMotorcycle.Id,
                    Identifier = newMotorcycle.Identifier,
                    Model = newMotorcycle.Model,
                    Plate = newMotorcycle.Plate,
                    Year = newMotorcycle.Year
                }
            );
            return Created("", newMotorcycle);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] string? plate)
    {
        IEnumerable<Motorcycle> motorcycles = _motorcycleBusiness.GetAll(plate);

        if (!string.IsNullOrEmpty(plate) && !motorcycles.Any())
            return NotFound(new { mensagem = "Placa não cadastrada!" });

        return Ok(motorcycles);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        var motorcycle = _motorcycleBusiness.GetById(id);
        if (motorcycle == null)
            return NotFound(new { mensagem = "Moto não encontrada!" });

        return Ok(motorcycle);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdatePlate(long id, [FromBody] string plate)
    {
        if (id <= 0)
            return BadRequest(new { mensagem = "Dados inválidos" });
        
        if (string.IsNullOrEmpty(plate))
            return BadRequest(new { mensagem = "Dados inválidos" });

        try
        {
            _motorcycleBusiness.UpdatePlate(id, plate);
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
            return StatusCode(500, new { mensagem = "Ocorreu um erro interno no servidor." });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        try
        {
            _motorcycleBusiness.Delete(id);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
    }
}

