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
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        public bool IsMale{ get; set; }

        public DateTime BirthDate { get; set; }

        public string Country { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public string ImagePath { get ; set; } = string.Empty;

    }
}
