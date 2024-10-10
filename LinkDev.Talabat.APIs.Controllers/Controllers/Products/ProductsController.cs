using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : ApiControllerBase
    {

        [HttpGet] // GET : /api/products
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {     
            var products = await serviceManager.ProductService.GetProductsAsync();
            return Ok(products);
        }


        [HttpGet("{id:int}")] // GET : /api/products/id
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }


        [HttpGet("brands")] // GET : /api/products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }


        [HttpGet("categories")] // GET : /api/products/categories
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await serviceManager.ProductService.GetCategoriesAsync();
            return Ok(categories);
        }

    }
}
