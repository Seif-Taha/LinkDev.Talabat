﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Products.Models
{
    public class ProductSpecParams
    {
        private string? search;
        private const int MaxPageSize = 10;
        private int pageSize = 5;


        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize 
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        
        }

        public string? Search
        {
            get { return search; }
            set { search = value?.ToUpper(); }
        }

    }
}
