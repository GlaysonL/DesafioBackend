using DesafioBackend.Business;
using DesafioBackend.Model;
using DesafioBackend.Model.DTO;
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
       

        [HttpPost("{id}/cnh")]
        [ProducesResponseType(typeof(ResponseDTO), 201)]
        [ProducesResponseType(typeof(ResponseDTO), 400)]
        public IActionResult UploadCnhImage(long id, [FromBody] CnhResquestDTO request)
        {
            string? imagem_cnh = request?.ImagemCnh != null ? request.ImagemCnh.ToString() : null;
            if (string.IsNullOrEmpty(imagem_cnh))
                return BadRequest(new ResponseDTO { Mensagem = "Dados inválidos" });
            
            try
            {
                Convert.FromBase64String(imagem_cnh);
            }
            catch (FormatException)
            {
                return BadRequest(new ResponseDTO { Mensagem = "A imagem da CNH deve estar em base64." });
            }
            _deliveryDriverBusiness.UploadCnhImage(id, imagem_cnh);
            return Created("", new ResponseDTO { Mensagem = "Foto da CNH enviada com sucesso" });
        }
       
    }
}