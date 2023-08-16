using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.DataAccess.Utils
{
    public class PaginationParams
    {
        public int PageNumber { get ; set; }
        public int PageSize { get; set; }
        public PaginationParams(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int GetSkipCount()
        {
            return (PageNumber - 1) * PageSize;
        }
    }
}
