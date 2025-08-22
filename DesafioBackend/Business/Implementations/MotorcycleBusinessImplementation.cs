using DesafioBackend.Model;
using DesafioBackend.Model.Context;
using DesafioBackend.Repository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Numerics;

namespace DesafioBackend.Business.Implementations
{
    public class MotorcycleBusinessImplementation : IMotorcycleBusiness
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        public MotorcycleBusinessImplementation(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }
        public Motorcycle UpdatePlate(long id, string plate)
        {
            return _motorcycleRepository.UpdatePlate(id, plate);            
        }
        public Motorcycle Register(Motorcycle motorcycle)
        {            
            if (_motorcycleRepository.GetAll(motorcycle.Plate).Any())
            {
                throw new InvalidOperationException($"Já existe uma moto cadastrada com a placa {motorcycle.Plate}");
            }
            return _motorcycleRepository.Register(motorcycle);
        }
        public Motorcycle GetById(long id)
        {
            var motorcycle = _motorcycleRepository.GetById(id);
            if (motorcycle == null) throw new KeyNotFoundException("Moto não encontrada");
            return motorcycle;
        }
        public IEnumerable<Motorcycle> GetAll(string? plate)
        {
            return _motorcycleRepository.GetAll(plate);
        }
        public void Delete(long id)
        {
            _motorcycleRepository.Delete(id);
        }
    }
}
