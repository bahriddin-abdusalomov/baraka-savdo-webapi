using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Exceptions.Suppliers
{
    public class SupplierNotFoundException : NotFoundException
    {
        public SupplierNotFoundException()
        {
            this.TitleMessage = "Supplier not found !";
        }
    }
}
