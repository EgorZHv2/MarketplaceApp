using Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PageModelDTO<TEntity>> ToPageModelAsync<TEntity>(this IQueryable<TEntity> entities, int pagenumber, int pagesize, CancellationToken cancellationToken = default)
        {
            PageModelDTO<TEntity> result = new PageModelDTO<TEntity>()
            {
                Values = await entities.Skip(pagenumber - 1).Take(pagesize).ToListAsync(cancellationToken),
                ItemsOnPage = pagesize,
                CurrentPage = pagenumber,
                TotalItems = entities.Count(),
                TotalPages = (int)Math.Ceiling(entities.Count() / (double)pagesize)
            };
            return result;
        }
    }
}