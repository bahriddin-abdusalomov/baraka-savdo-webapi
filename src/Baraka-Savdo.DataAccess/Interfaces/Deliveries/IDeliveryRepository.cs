using Baraka_Savdo.DataAccess.Common.Interfaces;
using Baraka_Savdo.DataAccess.ViewModels.Deliveries;
using Baraka_Savdo.Domain.Entities.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Interfaces.Dileveries
{
    internal interface IDeliveryRepository : IRepository<Delivery, Delivery>, IGetAll<DeliveryViewModel>
    {
        public Task<DeliveryViewModel> GetDeliverAsync(long id);
    }
}
