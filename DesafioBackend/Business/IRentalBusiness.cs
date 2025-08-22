using DesafioBackend.Model;

namespace DesafioBackend.Business
{
    public interface IRentalBusiness
    {
        Rental Register(Rental rental);
        Rental GetById(long id);
        void RegisterReturn(long id, System.DateTime returnDate);
    }
}
