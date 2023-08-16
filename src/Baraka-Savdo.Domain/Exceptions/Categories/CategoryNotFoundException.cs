using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Exceptions.Categories
{
    internal class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException() 
        {
            this.TitleMessage = "Cotegory not found !";
        }
    }
}
