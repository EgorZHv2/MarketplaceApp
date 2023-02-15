using Data.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class ICollectionExtensions
    {
        public static  PageModelDTO<TEntity> ToPageModel<TEntity>(this ICollection<TEntity> entities, int pagenumber, int pagesize)
        {
            PageModelDTO<TEntity> result = new PageModelDTO<TEntity>()
            {
                Values =  entities.Skip((pagenumber - 1)*pagesize).Take(pagesize),
                ItemsOnPage = pagesize,
                CurrentPage = pagenumber,
                TotalItems = entities.Count(),
                TotalPages = (int)Math.Ceiling(entities.Count() / (double)pagesize)
            };
            return result;
        }
    }
}
