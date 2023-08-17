using Baraka_Savdo.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Service.Common.Helpers
{
    public class TimeHelper
    {
       public static DateTime GetDateTime ()
        {
            var dtTime = DateTime.Now;
            dtTime.AddHours(TimeConstans.UTC);
            return dtTime;
        }
    }
}
