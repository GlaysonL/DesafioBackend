using System.Collections.Generic;
using System.Linq;
using DesafioBackend.Model;
using DesafioBackend.Model.Context;

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
            return _context.Locacoes.FirstOrDefault(l => l.Id == id)
                ?? throw new KeyNotFoundException("Locação não encontrada");
        }

        public void RegisterReturn(long id, System.DateTime returnDate)
        {
            var rental = GetById(id);
            rental.ReturnDate = returnDate;

            int expectedDays = (rental.ExpectedEndDate - rental.StartDate).Days + 1;
            int actualDays = (returnDate - rental.StartDate).Days + 1;
            decimal totalValue = 0m;

            if (returnDate < rental.ExpectedEndDate)
            {
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
                int extraDays = (returnDate - rental.ExpectedEndDate).Days;
                totalValue = expectedDays * rental.DailyRate + (extraDays * 50.00m);
            }
            else
            {
                totalValue = expectedDays * rental.DailyRate;
            }

            rental.DailyRate = totalValue / actualDays;
            _context.SaveChanges();
        }
    }
}
