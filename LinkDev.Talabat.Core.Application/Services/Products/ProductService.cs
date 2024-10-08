using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync() => mapper.Map<IEnumerable<BrandDto>>(await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync() => mapper.Map<IEnumerable<CategoryDto>>(await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());

        public async Task<ProductToReturnDto> GetProductAsync(int id) => mapper.Map<ProductToReturnDto>(await unitOfWork.GetRepository<Product, int>().GetAsync(id));

        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync() => mapper.Map<IEnumerable<ProductToReturnDto>>(await unitOfWork.GetRepository<Product, int>().GetAllAsync());

    }
}
