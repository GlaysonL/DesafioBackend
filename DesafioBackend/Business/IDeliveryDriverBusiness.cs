using DesafioBackend.Model;
using System.Collections.Generic;

namespace DesafioBackend.Business
{
    public interface IDeliveryDriverBusiness
    {
        DeliveryDriver Register(DeliveryDriver deliveryDriver);
        DeliveryDriver GetById(long id);
        IEnumerable<DeliveryDriver> GetAll();
        void UploadCnhImage(long id, string cnhImage);
        void Delete(long id);
        void SaveCnhImageLocally(long deliveryDriverId, string fileName, byte[] fileBytes);
    }
}