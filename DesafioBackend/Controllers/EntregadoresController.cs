using DesafioBackend.Business;
using DesafioBackend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesafioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntregadoresController : ControllerBase
    {
        private readonly IEntregadorBusiness _entregadorBusiness;
        public EntregadoresController(IEntregadorBusiness entregadorBusiness)
        {
            _entregadorBusiness = entregadorBusiness;
        }
        [HttpPost]
        public IActionResult CadastrarEntregador([FromBody] Entregador entregador)
        {
            if (entregador == null)
                return BadRequest(new { mensagem = "Dados inválidos" });
            var novo = _entregadorBusiness.CadastrarEntregador(entregador);
            return Created("", novo);
        }
        [HttpGet]
        public IActionResult ConsultarEntregadores()
        {
            var lista = _entregadorBusiness.ConsultarEntregadores();
            return Ok(lista);
        }
        [HttpGet("{id}")]
        public IActionResult ConsultarPorId(long id)
        {
            var entregador = _entregadorBusiness.ConsultaPorId(id);
            if (entregador == null)
                return NotFound(new { mensagem = "Entregador não encontrado" });
            return Ok(entregador);
        }
        [HttpPost("{id}/cnh")]
        public IActionResult EnviarFotoCnh(long id, [FromBody] string imagemCnh)
        {
            if (string.IsNullOrEmpty(imagemCnh))
                return BadRequest(new { mensagem = "Dados inválidos" });
            _entregadorBusiness.EnviarFotoCnh(id, imagemCnh);
            return Created("", new { mensagem = "Foto da CNH enviada com sucesso" });
        }
        [HttpDelete("{id}")]
        public IActionResult DeletarEntregador(long id)
        {
            _entregadorBusiness.DeletarEntregador(id);
            return Ok();
        }
    }
}