using DesafioBackend.Business;
using DesafioBackend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesafioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocacaoController : ControllerBase
    {
        private readonly ILocacaoBusiness _locacaoBusiness;
        public LocacaoController(ILocacaoBusiness locacaoBusiness)
        {
            _locacaoBusiness = locacaoBusiness;
        }
        [HttpPost]
        public IActionResult CadastrarLocacao([FromBody] Locacao locacao)
        {
            if (locacao == null)
                return BadRequest(new { mensagem = "Dados inválidos" });
            var nova = _locacaoBusiness.CadastrarLocacao(locacao);
            return Created("", nova);
        }
        [HttpGet]
        public IActionResult ConsultarLocacoes()
        {
            var lista = _locacaoBusiness.ConsultarLocacoes();
            return Ok(lista);
        }
        [HttpGet("{id}")]
        public IActionResult ConsultarPorId(long id)
        {
            var locacao = _locacaoBusiness.ConsultaPorId(id);
            if (locacao == null)
                return NotFound(new { mensagem = "Locação não encontrada" });
            return Ok(locacao);
        }
        [HttpPut("{id}/devolucao")]
        public IActionResult InformarDevolucao(long id, [FromBody] System.DateTime dataDevolucao)
        {
            _locacaoBusiness.InformarDevolucao(id, dataDevolucao);
            return Ok(new { mensagem = "Data de devolução informada com sucesso" });
        }
        [HttpDelete("{id}")]
        public IActionResult DeletarLocacao(long id)
        {
            _locacaoBusiness.DeletarLocacao(id);
            return Ok();
        }
    }
}