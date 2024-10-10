﻿using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product , int>
    {

        public ProductWithBrandAndCategorySpecifications()
            :base()
        {
            AddIncludes();
        }


        public ProductWithBrandAndCategorySpecifications(int id)
            :base(id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }


    }
}
