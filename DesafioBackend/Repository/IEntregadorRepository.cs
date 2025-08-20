using DesafioBackend.Model;
using System.Collections.Generic;

namespace DesafioBackend.Repository
{
    public interface IEntregadorRepository
    {
        Entregador CadastrarEntregador(Entregador entregador);
        Entregador ConsultaPorId(long id);
        IEnumerable<Entregador> ConsultarEntregadores();
        void EnviarFotoCnh(long id, string imagemCnh);
        void DeletarEntregador(long id);
    }
}