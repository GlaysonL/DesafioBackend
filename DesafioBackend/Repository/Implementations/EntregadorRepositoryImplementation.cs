using DesafioBackend.Model;
using DesafioBackend.Model.Context;
using System.Collections.Generic;
using System.Linq;

namespace DesafioBackend.Repository.Implementations
{
    public class EntregadorRepositoryImplementation : IEntregadorRepository
    {
        private readonly AppDbContext _context;
        public EntregadorRepositoryImplementation(AppDbContext context)
        {
            _context = context;
        }
        public Entregador CadastrarEntregador(Entregador entregador)
        {
            _context.Entregadores.Add(entregador);
            _context.SaveChanges();
            return entregador;
        }
        public Entregador ConsultaPorId(long id)
        {
            return _context.Entregadores.FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<Entregador> ConsultarEntregadores()
        {
            return _context.Entregadores.ToList();
        }
        public void EnviarFotoCnh(long id, string imagemCnh)
        {
            var entregador = _context.Entregadores.FirstOrDefault(e => e.Id == id);
            if (entregador == null) throw new KeyNotFoundException("Entregador não encontrado");
            entregador.ImagemCnh = imagemCnh;
            _context.SaveChanges();
        }
        public void DeletarEntregador(long id)
        {
            var entregador = _context.Entregadores.FirstOrDefault(e => e.Id == id);
            if (entregador == null) throw new KeyNotFoundException("Entregador não encontrado");
            _context.Entregadores.Remove(entregador);
            _context.SaveChanges();
        }
    }
}