using DesafioBackend.Model;

namespace DesafioBackend.Business
{
    public interface IDeliveryDriverBusiness
    {
        DeliveryDriver Register(DeliveryDriver deliveryDriver);
        void UploadCnhImage(long id, string cnhImage);
    }
}
