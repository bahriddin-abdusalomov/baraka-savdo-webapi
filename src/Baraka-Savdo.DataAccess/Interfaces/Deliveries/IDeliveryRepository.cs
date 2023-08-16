using Baraka_Savdo.DataAccess.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Interfaces.Dileveries
{
    internal interface IDeliveryRepository : IRepository<Deliver, Deliver>, IGetAll<DeliverViewModel>
    {
        public Task<DeliverViewModel> GetDeliverAsync(long id);
    }
}
