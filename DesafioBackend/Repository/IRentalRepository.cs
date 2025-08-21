using DesafioBackend.Model;
using System.Collections.Generic;

namespace DesafioBackend.Repository
{
    public interface IRentalRepository
    {
        Rental Register(Rental rental);
        Rental GetById(long id);      
        void RegisterReturn(long id, System.DateTime returnDate);     
    }
}