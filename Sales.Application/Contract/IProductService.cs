﻿using Sales.Application.Core;
using Sales.Application.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Contract
{
    public interface IProductService : IBaseServices<ProductsDtoAdd, ProductsDtoUpdate, ProductsDtoRemove >
    {
        object GetProductByProductID(int productID);
    }
}
