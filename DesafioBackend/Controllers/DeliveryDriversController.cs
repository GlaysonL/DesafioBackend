using DesafioBackend.Business;
using DesafioBackend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesafioBackend.Controllers
{
    [ApiController]
    [Route("entregadores")]
    public class DeliveryDriversController : ControllerBase
    {
        private readonly IDeliveryDriverBusiness _deliveryDriverBusiness;
        public DeliveryDriversController(IDeliveryDriverBusiness deliveryDriverBusiness)
        {
            _deliveryDriverBusiness = deliveryDriverBusiness;
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 500)]
        public IActionResult Register([FromBody] DeliveryDriver deliveryDriver)
        {
            if (deliveryDriver == null)
                return BadRequest(new { mensagem = "Dados inválidos" });
            try
            {
                _deliveryDriverBusiness.Register(deliveryDriver);
                return StatusCode(201, new { mensagem = "Entregador cadastrado com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao cadastrar entregador" });
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeliveryDriver>), 200)]
        public IActionResult GetAll()
        {
            var lista = _deliveryDriverBusiness.GetAll();
            return Ok(lista);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeliveryDriver), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public IActionResult GetById(long id)
        {
            var deliveryDriver = _deliveryDriverBusiness.GetById(id);
            if (deliveryDriver == null)
                return NotFound(new { mensagem = "Entregador não encontrado" });
            return Ok(deliveryDriver);
        }
        [HttpPost("{id}/cnh")]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(typeof(object), 400)]
        public IActionResult UploadCnhImage(long id, [FromBody] dynamic body)
        {
            string? imagem_cnh = body?.imagem_cnh != null ? body.imagem_cnh.ToString() : null;
            if (string.IsNullOrEmpty(imagem_cnh))
                return BadRequest(new { mensagem = "Dados inválidos" });
            _deliveryDriverBusiness.UploadCnhImage(id, imagem_cnh);
            return Created("", new { mensagem = "Foto da CNH enviada com sucesso" });
        }
        //[HttpDelete("{id}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult Delete(long id)
        //{
        //    if (id <= 0)
        //        return BadRequest(new { mensagem = "Dados inválidos" });
        //    _deliveryDriverBusiness.Delete(id);
        //    return Ok(new { mensagem = "Entregador removido com sucesso" });
        //}
    }
}