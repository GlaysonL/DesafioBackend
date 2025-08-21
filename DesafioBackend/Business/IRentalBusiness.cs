using DesafioBackend.Model;
using System.Collections.Generic;

namespace DesafioBackend.Business
{
    public interface IRentalBusiness
    {
        Rental Register(Rental rental);
        Rental GetById(long id);
        IEnumerable<Rental> GetAll();
        void RegisterReturn(long id, System.DateTime returnDate);
        void Delete(long id);
    }
}