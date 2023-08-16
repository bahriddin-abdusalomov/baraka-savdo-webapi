using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Entities.Discounts
{
    public class Discount : Auditable
    {
        [MaxLength(50)]
        public string Name { get ; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
