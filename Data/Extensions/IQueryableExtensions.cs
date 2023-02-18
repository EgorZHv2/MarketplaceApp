using Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PageModelDTO<TEntity>> ToPageModelAsync<TEntity>(this IQueryable<TEntity> entities, int pageNumber, int pageSize)
        {
            PageModelDTO<TEntity> result = new PageModelDTO<TEntity>()
            {
                Values = await entities.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(),
                ItemsOnPage = pageSize,
                CurrentPage = pageNumber,
                TotalItems = entities.Count(),
                TotalPages = (int)Math.Ceiling(entities.Count() / (double)pageSize)
            };
            return result;
        }
    }
}