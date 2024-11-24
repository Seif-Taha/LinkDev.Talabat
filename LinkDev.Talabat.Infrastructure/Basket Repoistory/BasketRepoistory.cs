using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Basket_Repoistory
{
    internal class BasketRepoistory : IBasketRepoistory
    {

        private readonly IDatabase _database;
        public BasketRepoistory(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);

        }

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(basket);

            var updated = await _database.StringSetAsync(basket.Id ,value , timeToLive);

            if(updated) return basket;

            return null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deleted = await _database.KeyDeleteAsync(id);
            return deleted;
        }

    }
}
