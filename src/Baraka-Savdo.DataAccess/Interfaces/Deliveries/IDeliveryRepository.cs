namespace Baraka_Savdo.DataAccess.Interfaces.Dileveries;

internal interface IDeliveryRepository : IRepository<Delivery>, IGetAll<Delivery>
{
    public Task<Delivery> GetDeliverAsync(long id);
}
