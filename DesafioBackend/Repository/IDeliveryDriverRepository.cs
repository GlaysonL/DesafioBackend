using DesafioBackend.Model;
using System.Collections.Generic;

namespace DesafioBackend.Repository
{
    public interface IDeliveryDriverRepository
    {
        DeliveryDriver Register(DeliveryDriver deliveryDriver);
        DeliveryDriver GetById(long id);
        IEnumerable<DeliveryDriver> GetAll();
        void UploadCnhImage(long id, string cnhImage);
        void Delete(long id);
    }
}