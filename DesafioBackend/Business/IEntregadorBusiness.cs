using DesafioBackend.Model;
using System.Collections.Generic;

namespace DesafioBackend.Business
{
    public interface IEntregadorBusiness
    {
        Entregador CadastrarEntregador(Entregador entregador);
        Entregador ConsultaPorId(long id);
        IEnumerable<Entregador> ConsultarEntregadores();
        void EnviarFotoCnh(long id, string imagemCnh);
        void DeletarEntregador(long id);
    }
}