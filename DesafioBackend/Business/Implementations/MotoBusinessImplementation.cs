using DesafioBackend.Model;
using DesafioBackend.Model.Context;
using DesafioBackend.Repository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Numerics;

namespace DesafioBackend.Business.Implementations
{
    public class MotoBusinessImplementation : IMotoBusiness
    {
        private readonly IMotoRepository _motoRepository;
        public MotoBusinessImplementation(IMotoRepository motoRepository)
        {
            _motoRepository = motoRepository;
        }
        public Moto ModificarPlaca(long id, string novaPlaca)
        {
            return _motoRepository.ModificarPlaca(id, novaPlaca);            
        }
        public Moto CadastrarMoto(Moto moto)
        {           
            return _motoRepository.CadastrarMoto(moto);
        }
        public Moto ConsultaPorId(long id)
        {
            return _motoRepository.ConsultaPorId(id);
        }
        public IEnumerable<Moto> ConsultarMotos(string? placa)
        {
         return _motoRepository.ConsultarMotos(placa);
        }
        public void DeletarMoto(long id)
        {
           _motoRepository.DeletarMoto(id);
        }
    }
}
