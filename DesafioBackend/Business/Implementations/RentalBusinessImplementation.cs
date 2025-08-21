using DesafioBackend.Model;
using DesafioBackend.Repository;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DesafioBackend.Business.Implementations
{
    public class RentalBusinessImplementation : IRentalBusiness
    {
        private readonly IRentalRepository _rentalRepository;
        public RentalBusinessImplementation(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }
        public Rental Register(Rental rental)
        {
            // Validação dos campos obrigatórios
            if (rental.StartDate == default || rental.EndDate == default || rental.ExpectedEndDate == default)
                throw new InvalidOperationException("Data de início, término e previsão de término são obrigatórias.");

            // Validação dos planos permitidos
            var planosPermitidos = new Dictionary<int, decimal>
            {
                { 7, 30.00m },
                { 15, 28.00m },
                { 30, 22.00m },
                { 45, 20.00m },
                { 50, 18.00m }
            };
            if (!planosPermitidos.ContainsKey(rental.Plan))
                throw new InvalidOperationException("Plano de locação inválido.");

            // Definir valor da diária conforme plano
            rental.DailyRate = planosPermitidos[rental.Plan];

            // Verificar se entregador está habilitado na categoria A
            var cnhType = rental.DeliveryDriver?.CnhType;
            if (cnhType == null)
            {
                throw new InvalidOperationException("Entregador não encontrado ou não informado.");
            }
            if (!(cnhType.Contains("A")))
            {
                throw new InvalidOperationException("Somente entregadores habilitados na categoria A podem efetuar uma locação.");
            }

            return _rentalRepository.Register(rental);
        }
        public Rental GetById(long id)
        {
            return _rentalRepository.GetById(id);
        }
        public IEnumerable<Rental> GetAll()
        {
            return _rentalRepository.GetAll();
        }
        public void RegisterReturn(long id, System.DateTime returnDate)
        {
            _rentalRepository.RegisterReturn(id, returnDate);
        }
        public void Delete(long id)
        {
            _rentalRepository.Delete(id);
        }
    }
}