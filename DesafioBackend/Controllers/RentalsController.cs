using DesafioBackend.Business;
using DesafioBackend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesafioBackend.Controllers
{
    [ApiController]
    [Route("locacao")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalBusiness _rentalBusiness;
        public RentalsController(IRentalBusiness rentalBusiness)
        {
            _rentalBusiness = rentalBusiness;
        }
        [HttpPost]
        [ProducesResponseType(typeof(Rental), 201)]
        [ProducesResponseType(typeof(ResponseDTO), 400)]
        public IActionResult Register([FromBody] Rental rental)
        {
            if (rental == null)
                return BadRequest(new { mensagem = "Dados inválidos" });
            var newRental = _rentalBusiness.Register(rental);
            return Created("", newRental);
        }
     
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Rental), 200)]
        [ProducesResponseType(typeof(ResponseDTO), 404)]
        public IActionResult GetById(long id)
        {
            var rental = _rentalBusiness.GetById(id);
            if (rental == null)
                return NotFound(new { mensagem = "Locação não encontrada" });
            return Ok(rental);
        }
        [HttpPut("{id}/devolucao")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(ResponseDTO), 400)]
        public IActionResult RegisterReturn(long id, [FromBody] System.DateTime returnDate)
        {
            _rentalBusiness.RegisterReturn(id, returnDate);
            return Ok(new { mensagem = "Data de devolução informada com sucesso" });
        }
     
    }
}