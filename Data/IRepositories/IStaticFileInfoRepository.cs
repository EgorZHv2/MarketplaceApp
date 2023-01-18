﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IStaticFileInfoRepository:IBaseRepository<StaticFileInfo>
    {
        Task<StaticFileInfo> GetByParentId(Guid Id);
    }
}