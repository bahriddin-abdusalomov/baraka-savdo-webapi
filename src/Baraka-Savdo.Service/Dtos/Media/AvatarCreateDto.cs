﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Service.Dtos.Media
{
    public class AvatarCreateDto
    {
        public IFormFile avatar { get; set; } = default!;
    }
}
