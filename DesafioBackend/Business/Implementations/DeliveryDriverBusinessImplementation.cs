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
            var validTypes = new[] { "A", "B", "AB" };
            if (string.IsNullOrWhiteSpace(deliveryDriver.CnhType) || !validTypes.Contains(deliveryDriver.CnhType))
            {
                throw new InvalidOperationException("O tipo de CNH deve ser A, B ou AB.");
            }
            return _deliveryDriverRepository.Register(deliveryDriver);
        }
       
        public void UploadCnhImage(long id, string cnhImage)
        {
            _deliveryDriverRepository.UploadCnhImage(id, cnhImage);
        }     
             
    }
}