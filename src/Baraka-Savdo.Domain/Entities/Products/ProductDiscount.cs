using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Entities.Products
{
    public class ProductDiscount :Auditable
    {
        public long ProductId { get; set; }

        public long DiscountId { get; set;}

        public string Description { get; set; } = string.Empty;

        public int Percentage { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }
    }
}
