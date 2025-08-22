using DesafioBackend.Model;

namespace DesafioBackend.Business
{
    public interface IMotorcycleBusiness
    {
        Motorcycle Register(Motorcycle motorcycle);
        Motorcycle GetById(long id);
        IEnumerable<Motorcycle> GetAll(string? plate);
        Motorcycle UpdatePlate(long id, string plate);
        void Delete(long id);
    }
}
