namespace Baraka_Savdo.DataAccess.Interfaces.Dileveries;

internal interface IDeliveryRepository : IRepository<Delivery, Delivery>, IGetAll<DeliveryViewModel>
{
    public Task<DeliveryViewModel> GetDeliverAsync(long id);
}
