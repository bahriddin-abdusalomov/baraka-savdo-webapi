using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Entities.Orders
{
    public class OrderDetail : Auditable
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double ResultPrice { get; set;}
    }
}
