using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode StatusCode { get; }  = HttpStatusCode.NotFound;

        public string TitleMessage { get; protected set; } = string.Empty;
    }
}
