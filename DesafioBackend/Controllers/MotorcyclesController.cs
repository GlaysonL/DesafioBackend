using DesafioBackend.Business;
using DesafioBackend.Messaging;
using DesafioBackend.Model;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackend.Controllers;

[ApiController]
[Route("motos")]
public class MotorcyclesController : ControllerBase
{
    private readonly ILogger<MotorcyclesController> _logger;
    private IMotorcycleBusiness _motorcycleBusiness;

    public MotorcyclesController(
        ILogger<MotorcyclesController> logger,
        IMotorcycleBusiness motorcycleBusiness
    )
    {
        _logger = logger;
        _motorcycleBusiness = motorcycleBusiness;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Motorcycle), 201)]
    [ProducesResponseType(typeof(ResponseDTO), 400)]
    [ProducesResponseType(typeof(ResponseDTO), 409)]
    public IActionResult Register([FromBody] Motorcycle motorcycle)
    {
        if (
            motorcycle == null
            || string.IsNullOrEmpty(motorcycle.Identifier)
            || string.IsNullOrEmpty(motorcycle.Model)
            || string.IsNullOrEmpty(motorcycle.Plate)
            || motorcycle.Year <= 0
        )
        {
            return BadRequest(new ResponseDTO { Mensagem = "Dados inválidos" });
        }

        try
        {
            var newMotorcycle = _motorcycleBusiness.Register(motorcycle);
            
            var publisher = new MotorcycleRegisteredPublisher();
          
            publisher.Publish(newMotorcycle);
            _logger.LogInformation(
                "Moto criada com sucesso: {@Motorcycle}",
                new
                {
                    Id = newMotorcycle.Id,
                    Identifier = newMotorcycle.Identifier,
                    Model = newMotorcycle.Model,
                    Plate = newMotorcycle.Plate,
                    Year = newMotorcycle.Year,
                }
            );
            return Created("", newMotorcycle);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ResponseDTO { Mensagem = ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Motorcycle>), 200)]
    [ProducesResponseType(typeof(ResponseDTO), 404)]
    public IActionResult GetAll([FromQuery] string? plate)
    {
        IEnumerable<Motorcycle> motorcycles = _motorcycleBusiness.GetAll(plate);

        if (!string.IsNullOrEmpty(plate) && !motorcycles.Any())
            return NotFound(new { mensagem = "Placa n�o cadastrada!" });

        return Ok(motorcycles);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Motorcycle), 200)]
    [ProducesResponseType(typeof(ResponseDTO), 404)]
    public IActionResult GetById(long id)
    {
        try
        {
            var motorcycle = _motorcycleBusiness.GetById(id);
           return Ok(motorcycle);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }       
    }

    [HttpPut("{id}/placa")]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(typeof(ResponseDTO), 400)]
    [ProducesResponseType(typeof(ResponseDTO), 404)]
    [ProducesResponseType(typeof(ResponseDTO), 409)]
    [ProducesResponseType(typeof(ResponseDTO), 500)]
    public IActionResult UpdatePlate(long id, [FromBody] dynamic body)
    {
         string? plate = body?.placa != null ? body.placa.ToString() : null;
        if (id <= 0 || string.IsNullOrEmpty(plate))
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
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(typeof(ResponseDTO), 400)]
    [ProducesResponseType(typeof(ResponseDTO), 404)]
    [ProducesResponseType(typeof(ResponseDTO), 409)]
    public IActionResult Delete(long id)
    {
        if (id <= 0)
            return BadRequest(new { mensagem = "Dados inválidos" });

        try
        {
            _motorcycleBusiness.Delete(id);
            return Ok(new { mensagem = "Moto removida com sucesso" });
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
