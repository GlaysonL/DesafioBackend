using DesafioBackend.Model;
using DesafioBackend.Model.Context;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Numerics;

namespace DesafioBackend.Repository.Implementations
{
    public class MotorcycleRepositoryImplementation : IMotorcycleRepository
    {
        private AppDbContext _context;

        public MotorcycleRepositoryImplementation(AppDbContext context)
        {
            _context = context;
        }
        public Motorcycle UpdatePlate(long id, string plate)
        {       
            var existingMotorcycle = _context.Motos
                .FirstOrDefault(m => m.Id == id);

            if (existingMotorcycle == null)
            {
                throw new KeyNotFoundException($"Não foi encontrada moto com o ID {id}");
            }          

            if (_context.Motos.Any(m => m.Plate.Equals(plate) && m.Id != id))
            {
                throw new InvalidOperationException($"Já existe uma moto cadastrada com a placa {plate}");
            }

            var previousValue = existingMotorcycle.Plate;
            existingMotorcycle.Plate = plate;

            try
            {                
                _context.SaveChanges();                            
            }
            catch (Exception)
            {
                throw;
            }
            return existingMotorcycle;
        }       

        public Motorcycle Register(Motorcycle motorcycle)
        {
            if (_context.Motos.Any(m => m.Plate == motorcycle.Plate))
                throw new InvalidOperationException($"Já existe uma moto cadastrada com a placa {motorcycle.Plate}");

            try
            {
                _context.Add(motorcycle);
                _context.SaveChanges();
               
            }
            catch (Exception)
            {
                throw;
            }
            
            return motorcycle;          
        }

        public Motorcycle GetById(long id)
        {
            return _context.Motos
                .FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Motorcycle> GetAll(string? plate)
        {
            return _context.Motos
                .Where(m => string.IsNullOrEmpty(plate) || m.Plate.Equals(plate))                
                .ToList();
        }

        public void Delete(long id)
        {
            var motorcycle = _context.Motos.SingleOrDefault(m => m.Id == id);
            if (motorcycle == null)
            {
                throw new KeyNotFoundException("Dados inválidos");
            }
            
            bool hasRental = _context.Locacoes.Any(l => l.MotorcycleId == id);
            if (hasRental)
            {
                throw new InvalidOperationException("Não é possível remover a moto: existem locações associadas.");
            }
            try
            {
                var previousValue = System.Text.Json.JsonSerializer.Serialize(motorcycle);
                _context.Motos.Remove(motorcycle);
                _context.SaveChanges();                           
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
