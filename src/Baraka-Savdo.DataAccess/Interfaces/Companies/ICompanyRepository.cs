using Baraka_Savdo.DataAccess.Common.Interfaces;
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
