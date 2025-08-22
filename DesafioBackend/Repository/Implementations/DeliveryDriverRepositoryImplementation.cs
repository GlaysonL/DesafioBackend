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
      
        public void UploadCnhImage(long id, string cnhImage)
        {
            var deliveryDriver = _context.Entregadores.FirstOrDefault(e => e.Id == id);
            if (deliveryDriver == null) throw new KeyNotFoundException("Entregador n�o encontrado");

            // Decodifica a imagem base64
            byte[] imageBytes = Convert.FromBase64String(cnhImage);
            string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "UploadDir");
            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);
            string timestamp = DateTime.Now.ToString("yyyyMMddTHHmmss");
            string fileName = $"cnh_{id}_{timestamp}.png";
            string filePath = Path.Combine(uploadDir, fileName);

            File.WriteAllBytes(filePath, imageBytes);

            // Salva o caminho do arquivo no banco
            deliveryDriver.CnhImage = filePath;
            _context.SaveChanges();
        }
       
        public IEnumerable<DeliveryDriver> GetAll()
        {
            return _context.Entregadores.ToList();
        }
    }
}