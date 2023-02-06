using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class IQueryableExtensions
    {
        public async static Task<PageModel<TEntity>> ToPageModelAsync<TEntity>(this IQueryable<TEntity> entities, int pagenumber, int pagesize, CancellationToken cancellationToken = default)
        {
             PageModel<TEntity> result = new PageModel<TEntity>()
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
