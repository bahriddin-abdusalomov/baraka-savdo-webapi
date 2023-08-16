using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Exceptions.Companies
{
    public class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException() 
        {
            this.TitleMessage = "Company not found !";
        }
    }
}
