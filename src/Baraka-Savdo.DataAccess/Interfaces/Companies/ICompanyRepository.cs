using Baraka_Savdo.DataAccess.Common.Interfaces;
using Baraka_Savdo.Domain.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Interfaces.Companies
{
    public interface ICompanyRepository : IRepository<Company, Company>, IGetAll<Company>
    {
    }
}
