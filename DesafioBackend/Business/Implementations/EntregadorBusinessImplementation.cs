using DesafioBackend.Model;
using DesafioBackend.Repository;
using System.Collections.Generic;

namespace DesafioBackend.Business.Implementations
{
    public class EntregadorBusinessImplementation : IEntregadorBusiness
    {
        private readonly IEntregadorRepository _entregadorRepository;
        public EntregadorBusinessImplementation(IEntregadorRepository entregadorRepository)
        {
            _entregadorRepository = entregadorRepository;
        }
        public Entregador CadastrarEntregador(Entregador entregador)
        {
            return _entregadorRepository.CadastrarEntregador(entregador);
        }
        public Entregador ConsultaPorId(long id)
        {
            return _entregadorRepository.ConsultaPorId(id);
        }
        public IEnumerable<Entregador> ConsultarEntregadores()
        {
            return _entregadorRepository.ConsultarEntregadores();
        }
        public void EnviarFotoCnh(long id, string imagemCnh)
        {
            _entregadorRepository.EnviarFotoCnh(id, imagemCnh);
        }
        public void DeletarEntregador(long id)
        {
            _entregadorRepository.DeletarEntregador(id);
        }
    }
}