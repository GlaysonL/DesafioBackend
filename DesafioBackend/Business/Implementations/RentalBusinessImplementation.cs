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
            if (rental.StartDate == default || rental.EndDate == default || rental.ExpectedEndDate == default)
                throw new InvalidOperationException("A data de início, término e previsão de término da locação são obrigatórias.");
            var creationDate = DateTime.Now.Date;
            var expectedStartDate = creationDate.AddDays(1);
            if (rental.StartDate.Date != expectedStartDate)
                throw new InvalidOperationException($"A data de início da locação deve ser o primeiro dia após a data de criação ({expectedStartDate:yyyy-MM-dd}).");
            var allowedPlans = new Dictionary<int, decimal>
            {
                { 7, 30.00m },
                { 15, 28.00m },
                { 30, 22.00m },
                { 45, 20.00m },
                { 50, 18.00m }
            };
            if (!allowedPlans.ContainsKey(rental.Plan))
                throw new InvalidOperationException("Plano de locação inválido. Os planos permitidos são: 7, 15, 30, 45 ou 50 dias.");
            
            rental.DailyRate = allowedPlans[rental.Plan];
            var cnhType = rental.DeliveryDriver?.CnhType;
            if (string.IsNullOrWhiteSpace(cnhType))
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
        public void RegisterReturn(long id, System.DateTime returnDate)
        {
            _rentalRepository.RegisterReturn(id, returnDate);
        }     
    }
}