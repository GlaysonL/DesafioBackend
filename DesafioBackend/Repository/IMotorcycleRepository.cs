using DesafioBackend.Model;

namespace DesafioBackend.Repository
{
    public interface IMotorcycleRepository
    {
        Motorcycle Register(Motorcycle motorcycle);
        Motorcycle GetById(long id);
        IEnumerable<Motorcycle> GetAll(string? plate);
        Motorcycle UpdatePlate(long id, string plate);
        void Delete(long id);
    }
}
