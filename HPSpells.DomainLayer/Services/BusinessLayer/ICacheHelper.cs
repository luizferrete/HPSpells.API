using HPSpells.DomainLayer.DTOs;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSpells.DomainLayer.Services.BusinessLayer
{
    public interface ICacheHelper
    {
        Task<T> GetOrCreateAsync<T>(string cacheKey, Func<ICacheEntry, Task<T>> factoryMethod, CacheOptions? options = null);
        T GetOrCreate<T>(string cacheKey, Func<ICacheEntry, T> factoryMethod, CacheOptions? options = null);
        void Remove(string cacheKey);
    }
}
