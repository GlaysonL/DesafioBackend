using DesafioBackend.Model;
using DesafioBackend.Model.Context;
using System.Collections.Generic;
using System.Linq;

namespace DesafioBackend.Repository.Implementations
{
    public class RentalRepositoryImplementation : IRentalRepository
    {
        private readonly AppDbContext _context;
        public RentalRepositoryImplementation(AppDbContext context)
        {
            _context = context;
        }
        public Rental Register(Rental rental)
        {
            _context.Locacoes.Add(rental);
            _context.SaveChanges();
            return rental;
        }
        public Rental GetById(long id)
        {
            return _context.Locacoes.FirstOrDefault(l => l.Id == id);
        }
        public IEnumerable<Rental> GetAll()
        {
            return _context.Locacoes.ToList();
        }
        public void RegisterReturn(long id, System.DateTime returnDate)
        {
            var rental = _context.Locacoes.FirstOrDefault(l => l.Id == id);
            if (rental == null) throw new KeyNotFoundException("Loca��o n�o encontrada");
            rental.ReturnDate = returnDate;

            // C�lculo do valor total da loca��o
            int expectedDays = (rental.ExpectedEndDate - rental.StartDate).Days + 1;
            int actualDays = (returnDate - rental.StartDate).Days + 1;
            decimal totalValue = 0m;

            if (returnDate < rental.ExpectedEndDate)
            {
                // Devolu��o antecipada
                int unusedDays = expectedDays - actualDays;
                totalValue = actualDays * rental.DailyRate;
                decimal penalty = 0m;
                if (rental.Plan == 7)
                    penalty = unusedDays * rental.DailyRate * 0.20m;
                else if (rental.Plan == 15)
                    penalty = unusedDays * rental.DailyRate * 0.40m;
                totalValue += penalty;
            }
            else if (returnDate > rental.ExpectedEndDate)
            {
                // Devolu��o tardia
                int extraDays = (returnDate - rental.ExpectedEndDate).Days;
                totalValue = expectedDays * rental.DailyRate + (extraDays * 50.00m);
            }
            else
            {
                // Devolu��o no prazo
                totalValue = expectedDays * rental.DailyRate;
            }
            _context.SaveChanges();
        }
        public void Delete(long id)
        {
            var rental = _context.Locacoes.FirstOrDefault(l => l.Id == id);
            if (rental == null) throw new KeyNotFoundException("Loca��o n�o encontrada");
            _context.Locacoes.Remove(rental);
            _context.SaveChanges();
        }
    }
}