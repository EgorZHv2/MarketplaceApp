using Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PageModelDTO<TEntity>> ToPageModelAsync<TEntity>(this IQueryable<TEntity> entities, PaginationDTO pagination)
        {
            PageModelDTO<TEntity> result = new PageModelDTO<TEntity>()
            {
                Values = await entities.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync(),
                ItemsOnPage = pagination.PageSize,
                CurrentPage = pagination.PageNumber,
                TotalItems = entities.Count(),
                TotalPages = (int)Math.Ceiling(entities.Count() / (double)pagination.PageSize)
            };
            return result;
        }
    }
}