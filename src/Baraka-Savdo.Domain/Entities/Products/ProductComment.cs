using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Entities.Products
{
    public class ProductComment
    {
        public long ProductId { get ; set; }

        public long UserId { get; set; }

        public string Comment { get ; set; } = string.Empty;

        public bool IsEdit { get; set; }

    }
}
