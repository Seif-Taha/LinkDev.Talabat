using AutoMapper;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Basket.Models;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services.Basket
{
    public class BasketService(IBasketRepoistory basketRepoistory, IMapper mapper , IConfiguration configuration) : IBasketService
    {
        public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)
        {
            var basket = await basketRepoistory.GetAsync(basketId);

            if (basket is null)
                throw new NotFoundException();
                //throw new NotFoundException(nameof(CustomerBasket), basketId);

            return mapper.Map<CustomerBasketDto>(basket);

        }

        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);

            var timeToLive = TimeSpan.FromDays(double.Parse(configuration.GetSection("RedisSettings")["TimeToLiveInDays"]));

            var updatedBasket = await basketRepoistory.UpdateAsync(basket , timeToLive);

            if(updatedBasket is null)
                throw new Exception();
            //throw new BadRequestException("Can not Update");

            return basketDto;

        }

        public async Task DeleteCustomerBasketAsync(string basketId)
        {
            var deleted = await basketRepoistory.DeleteAsync(basketId);

            if(!deleted)
                throw new Exception();
                // throw new BadRequestException("unable to delete this basket");

        }

    }
}
