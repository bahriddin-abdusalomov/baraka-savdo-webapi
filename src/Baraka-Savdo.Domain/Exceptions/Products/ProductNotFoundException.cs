﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Domain.Exceptions.Products
{
    public class ProductNotFoundException : NotFoundException 
    {
        public ProductNotFoundException() 
        {
            this.TitleMessage = "Product not found !";
        }
    }
}
