using DesafioBackend.Model;

namespace DesafioBackend.Repository
{
    public interface IMotoRepository
    {
        Moto CadastrarMoto(Moto moto);
        Moto ConsultaPorId(long id);
        IEnumerable<Moto> ConsultarMotos(string? placa);
        Moto ModificarPlaca(long id, string placa);
        void DeletarMoto(long id);        
    }
}
