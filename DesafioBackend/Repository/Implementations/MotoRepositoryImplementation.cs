using DesafioBackend.Model;
using DesafioBackend.Model.Context;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Numerics;

namespace DesafioBackend.Repository.Implementations
{
    public class MotoRepositoryImplementation : IMotoRepository
    {
        private AppDbContext _context;

        public MotoRepositoryImplementation(AppDbContext context)
        {
            _context = context;
        }
        public Moto ModificarPlaca(long id, string novaPlaca)
        {       
            var motoExistente = _context.Motos
                .FirstOrDefault(m => m.Id == id);

            if (motoExistente == null)
            {
                throw new KeyNotFoundException($"Não foi encontrada moto com o ID {id}");
            }          

            if (_context.Motos.Any(m => m.Placa.Equals(novaPlaca) && m.Id != id))
            {
                throw new InvalidOperationException($"Já existe uma moto cadastrada com a placa {novaPlaca}");
            }

            motoExistente.Placa = novaPlaca;


            try
            {                
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return motoExistente;
        }       

        public Moto CadastrarMoto(Moto moto)
        {
            if (_context.Motos.Any(m => m.Placa == moto.Placa))
                throw new InvalidOperationException($"Já existe uma moto cadastrada com a placa {moto.Placa}");

            try
            {
                _context.Add(moto);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
            return moto;          
        }

        public Moto ConsultaPorId(long id)
        {
            return _context.Motos
                .FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Moto> ConsultarMotos(string? placa)
        {
         return _context.Motos
                .Where(m => string.IsNullOrEmpty(placa) || m.Placa.Equals(placa))                
                .ToList();
        }


        public void DeletarMoto(long id)
        {
           var moto = _context.Motos.SingleOrDefault(m => m.Id == id);
            if (moto == null)
            {
                throw new KeyNotFoundException($"Não foi encontrada moto com o ID {id}");
            }
            try
            {
                _context.Motos.Remove(moto);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
