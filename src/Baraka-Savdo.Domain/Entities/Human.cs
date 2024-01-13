using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Entities
{
    public abstract class Human : Auditable
    {
        [MaxLength(50)]
        public string? FirstName { get; set; } 

        [MaxLength(50)]
        public string? LastName { get; set; } 

        public bool? IsMale{ get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Country { get; set; }

        public string? Region { get; set; } 

        public string? ImagePath { get ; set; } 

    }
}
