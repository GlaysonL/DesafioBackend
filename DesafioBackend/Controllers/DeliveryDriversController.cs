using DesafioBackend.Business;
using DesafioBackend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesafioBackend.Controllers
{
    [ApiController]
    [Route("api/entregadores")]
    public class DeliveryDriversController : ControllerBase
    {
        private readonly IDeliveryDriverBusiness _deliveryDriverBusiness;
        public DeliveryDriversController(IDeliveryDriverBusiness deliveryDriverBusiness)
        {
            _deliveryDriverBusiness = deliveryDriverBusiness;
        }
        [HttpPost]
        public IActionResult Register([FromBody] DeliveryDriver deliveryDriver)
        {
            if (deliveryDriver == null)
                return BadRequest(new { mensagem = "Dados inválidos" });
            var novo = _deliveryDriverBusiness.Register(deliveryDriver);
            return Created("", novo);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var lista = _deliveryDriverBusiness.GetAll();
            return Ok(lista);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var deliveryDriver = _deliveryDriverBusiness.GetById(id);
            if (deliveryDriver == null)
                return NotFound(new { mensagem = "Entregador não encontrado" });
            return Ok(deliveryDriver);
        }
        [HttpPost("{id}/cnh")]
        public IActionResult UploadCnhImage(long id, [FromBody] string cnhImage)
        {
            if (string.IsNullOrEmpty(cnhImage))
                return BadRequest(new { mensagem = "Dados inválidos" });
            _deliveryDriverBusiness.UploadCnhImage(id, cnhImage);
            return Created("", new { mensagem = "Foto da CNH enviada com sucesso" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _deliveryDriverBusiness.Delete(id);
            return Ok();
        }
    }
}