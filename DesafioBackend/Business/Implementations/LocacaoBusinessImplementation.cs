using DesafioBackend.Model;
using DesafioBackend.Repository;
using System.Collections.Generic;

namespace DesafioBackend.Business.Implementations
{
    public class LocacaoBusinessImplementation : ILocacaoBusiness
    {
        private readonly ILocacaoRepository _locacaoRepository;
        public LocacaoBusinessImplementation(ILocacaoRepository locacaoRepository)
        {
            _locacaoRepository = locacaoRepository;
        }
        public Locacao CadastrarLocacao(Locacao locacao)
        {
            return _locacaoRepository.CadastrarLocacao(locacao);
        }
        public Locacao ConsultaPorId(long id)
        {
            return _locacaoRepository.ConsultaPorId(id);
        }
        public IEnumerable<Locacao> ConsultarLocacoes()
        {
            return _locacaoRepository.ConsultarLocacoes();
        }
        public void InformarDevolucao(long id, System.DateTime dataDevolucao)
        {
            _locacaoRepository.InformarDevolucao(id, dataDevolucao);
        }
        public void DeletarLocacao(long id)
        {
            _locacaoRepository.DeletarLocacao(id);
        }
    }
}