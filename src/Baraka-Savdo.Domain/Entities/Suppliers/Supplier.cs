using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Entities.Suppliers
{
    public class Supplier : Auditable
    {
        [MaxLength(50)]
        public string CompanyName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [MaxLength(13)]
        public string FaxPhoneNumber { get ; set; } = string.Empty;
        
        [MaxLength(13)]
        public string ContactPhoneNumber { get; set; } = string.Empty;

    }
}


