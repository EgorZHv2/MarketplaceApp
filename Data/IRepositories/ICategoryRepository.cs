﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface ICategoryRepository:IBaseDictionaryRepository<Category>
    {
        public Task<IQueryable<Category>> GetCategoriesWithChilds();
    }
}