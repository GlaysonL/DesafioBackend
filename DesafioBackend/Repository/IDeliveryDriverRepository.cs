using DesafioBackend.Model;
using System.Collections.Generic;

namespace DesafioBackend.Repository
{
    public interface IDeliveryDriverRepository
    {
    DeliveryDriver Register(DeliveryDriver deliveryDriver);

    void UploadCnhImage(long id, string cnhImage);

    IEnumerable<DeliveryDriver> GetAll();
    }
}