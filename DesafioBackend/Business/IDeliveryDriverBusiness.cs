using DesafioBackend.Model;
using System.Collections.Generic;

namespace DesafioBackend.Business
{
    public interface IDeliveryDriverBusiness
    {
        DeliveryDriver Register(DeliveryDriver deliveryDriver);   
        void UploadCnhImage(long id, string cnhImage);
      
    }
}