using DesafioBackend.Model;
using DesafioBackend.Repository;

namespace DesafioBackend.Business.Implementations
{
    public class DeliveryDriverBusinessImplementation : IDeliveryDriverBusiness
    {
        private readonly IDeliveryDriverRepository _deliveryDriverRepository;

        public DeliveryDriverBusinessImplementation(
            IDeliveryDriverRepository deliveryDriverRepository
        )
        {
            _deliveryDriverRepository = deliveryDriverRepository;
        }

        public DeliveryDriver Register(DeliveryDriver deliveryDriver)
        {
            var validTypes = new[] { "A", "B", "AB" };
            if (
                string.IsNullOrWhiteSpace(deliveryDriver.CnhType)
                || !validTypes.Contains(deliveryDriver.CnhType)
            )
            {
                throw new InvalidOperationException("O tipo de CNH deve ser A, B ou AB.");
            }

            var existingByCnpj = _deliveryDriverRepository
                .GetAll()
                .Any(d => d.Cnpj == deliveryDriver.Cnpj);
            if (existingByCnpj)
            {
                throw new InvalidOperationException(
                    $"Já existe um entregador cadastrado com o CNPJ {deliveryDriver.Cnpj}"
                );
            }

            var existingByCnh = _deliveryDriverRepository
                .GetAll()
                .Any(d => d.CnhNumber == deliveryDriver.CnhNumber);
            if (existingByCnh)
            {
                throw new InvalidOperationException(
                    $"Já existe um entregador cadastrado com o número de CNH {deliveryDriver.CnhNumber}"
                );
            }

            return _deliveryDriverRepository.Register(deliveryDriver);
        }

        public void UploadCnhImage(long id, string cnhImage)
        {
            _deliveryDriverRepository.UploadCnhImage(id, cnhImage);
        }
    }
}
