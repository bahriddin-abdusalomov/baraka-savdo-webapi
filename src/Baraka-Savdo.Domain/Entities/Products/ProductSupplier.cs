using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Entities.Products
{
    public class ProductSupplier : Auditable
    {
        public long ProductId { get ; set; }  
        
        public long SupplierId { get; set;}
        
        public int Quantity { get; set; }   
        
        public double UnitPrice { get; set; }
        
        public string Description { get; set; } = string.Empty;
    }
}
