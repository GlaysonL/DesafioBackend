using DesafioBackend.Model;
using DesafioBackend.Model.Context;
using System.Collections.Generic;
using System.Linq;

namespace DesafioBackend.Repository.Implementations
{
    public class LocacaoRepositoryImplementation : ILocacaoRepository
    {
        private readonly AppDbContext _context;
        public LocacaoRepositoryImplementation(AppDbContext context)
        {
            _context = context;
        }
        public Locacao CadastrarLocacao(Locacao locacao)
        {
            _context.Locacoes.Add(locacao);
            _context.SaveChanges();
            return locacao;
        }
        public Locacao ConsultaPorId(long id)
        {
            return _context.Locacoes.FirstOrDefault(l => l.Id == id);
        }
        public IEnumerable<Locacao> ConsultarLocacoes()
        {
            return _context.Locacoes.ToList();
        }
        public void InformarDevolucao(long id, System.DateTime dataDevolucao)
        {
            var locacao = _context.Locacoes.FirstOrDefault(l => l.Id == id);
            if (locacao == null) throw new KeyNotFoundException("Locação não encontrada");
            locacao.DataDevolucao = dataDevolucao;
            _context.SaveChanges();
        }
        public void DeletarLocacao(long id)
        {
            var locacao = _context.Locacoes.FirstOrDefault(l => l.Id == id);
            if (locacao == null) throw new KeyNotFoundException("Locação não encontrada");
            _context.Locacoes.Remove(locacao);
            _context.SaveChanges();
        }
    }
}