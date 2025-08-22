using DesafioBackend.Model;

namespace DesafioBackend.Repository
{
    public interface IDeliveryDriverRepository
    {
        DeliveryDriver Register(DeliveryDriver deliveryDriver);

        void UploadCnhImage(long id, string cnhImage);

        IEnumerable<DeliveryDriver> GetAll();
    }
}
