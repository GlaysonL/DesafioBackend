using DesafioBackend.Model;
using DesafioBackend.Model.Context;

namespace DesafioBackend.Repository.Implementations
{
    public class DeliveryDriverRepositoryImplementation : IDeliveryDriverRepository
    {
        private readonly AppDbContext _context;
        public DeliveryDriverRepositoryImplementation(AppDbContext context)
        {
            _context = context;
        }
        public DeliveryDriver Register(DeliveryDriver deliveryDriver)
        {
            _context.Entregadores.Add(deliveryDriver);
            _context.SaveChanges();
            return deliveryDriver;
        }
        public DeliveryDriver GetById(long id)
        {
            return _context.Entregadores.FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<DeliveryDriver> GetAll()
        {
            return _context.Entregadores.ToList();
        }
        public void UploadCnhImage(long id, string cnhImage)
        {
            var deliveryDriver = _context.Entregadores.FirstOrDefault(e => e.Id == id);
            if (deliveryDriver == null) throw new KeyNotFoundException("Entregador não encontrado");
            deliveryDriver.CnhImage = cnhImage;
            _context.SaveChanges();
        }
        public void Delete(long id)
        {
            var deliveryDriver = _context.Entregadores.FirstOrDefault(e => e.Id == id);
            if (deliveryDriver == null) throw new KeyNotFoundException("Entregador não encontrado");
            _context.Entregadores.Remove(deliveryDriver);
            _context.SaveChanges();
        }
    }
}