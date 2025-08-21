using DesafioBackend.Model;
using DesafioBackend.Repository;

namespace DesafioBackend.Business.Implementations
{
    public class DeliveryDriverBusinessImplementation : IDeliveryDriverBusiness
    {
        private readonly IDeliveryDriverRepository _deliveryDriverRepository;
        public DeliveryDriverBusinessImplementation(IDeliveryDriverRepository deliveryDriverRepository)
        {
            _deliveryDriverRepository = deliveryDriverRepository;
        }
        public DeliveryDriver Register(DeliveryDriver deliveryDriver)
        {
            return _deliveryDriverRepository.Register(deliveryDriver);
        }
        public DeliveryDriver GetById(long id)
        {
            return _deliveryDriverRepository.GetById(id);
        }
        public IEnumerable<DeliveryDriver> GetAll()
        {
            return _deliveryDriverRepository.GetAll();
        }
        public void UploadCnhImage(long id, string cnhImage)
        {
            _deliveryDriverRepository.UploadCnhImage(id, cnhImage);
        }
        public void Delete(long id)
        {
            _deliveryDriverRepository.Delete(id);
        }
        public void SaveCnhImageLocally(long deliveryDriverId, string fileName, byte[] fileBytes)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            if (extension != ".png" && extension != ".bmp")
                throw new InvalidOperationException("A imagem da CNH deve ser PNG ou BMP.");
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "ImagensCnh");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var path = Path.Combine(folder, $"cnh_{deliveryDriverId}{extension}");
            File.WriteAllBytes(path, fileBytes);
        }          
    }
}