using DesafioBackend.Model;
using System.Collections.Generic;

namespace DesafioBackend.Business
{
    public interface ILocacaoBusiness
    {
        Locacao CadastrarLocacao(Locacao locacao);
        Locacao ConsultaPorId(long id);
        IEnumerable<Locacao> ConsultarLocacoes();
        void InformarDevolucao(long id, System.DateTime dataDevolucao);
        void DeletarLocacao(long id);
    }
}