using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql.Replication.TestDecoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class BaseService<TEntity,TDTO,TCreateDTO,TUpdateDTO>:IBaseService<TEntity,TDTO,TCreateDTO,TUpdateDTO> where TEntity:BaseEntity
    {
        protected IBaseRepository<TEntity> _repository;
        protected IMapper _mapper;
        protected ILogger _logger;
        public BaseService(IBaseRepository<TEntity> repository,
            IMapper mapper,
            ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public virtual async Task<Guid> Create(Guid userId,TCreateDTO createDTO,CancellationToken cancellationToken = default)
        {
            TEntity entity;
            try
            {
                entity = _mapper.Map<TEntity>(createDTO);
            }
            catch
            {
                throw new MappingException(this);
            }
            entity.CreateDateTime = DateTime.UtcNow;
            entity.CreatorId = userId;
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userId;
            entity.IsActive = true;
            entity.IsDeleted = false;
            Guid result =  await _repository.Create(entity,cancellationToken);
            return result;
            
        }
        public virtual async Task<List<TDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            List<TDTO> result = new List<TDTO>();
            var list = _repository.GetAll().Where(e => e.IsActive).AsQueryable();
          
         
            try
            {
                result = await _mapper.ProjectTo<TDTO>(list).ToListAsync(cancellationToken);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }
        public virtual async Task<TDTO> GetById(Guid Id,CancellationToken cancellationToken = default)
        {
            TDTO result;
            var entity = await _repository.GetById(Id,cancellationToken);
            try
            {
                result = _mapper.Map<TDTO>(entity);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;

        }
        public virtual async Task<PageModel<TDTO>> GetPage(FilterPagingModel pagingModel,CancellationToken cancellationToken = default)
        {
           PageModel<TDTO> result = new PageModel<TDTO>();
            var PageModel = _repository.GetPage(_repository.GetAll().AsQueryable(), pagingModel.PageNumber, pagingModel.PageSize).Result;
            result.CurrentPage = PageModel.CurrentPage;
            result.TotalPages = PageModel.TotalPages;
            result.ItemsOnPage = PageModel.ItemsOnPage;
            result.TotalItems = PageModel.TotalItems;
            try
            {
                result.Values = _mapper.Map<IEnumerable<TDTO>>(PageModel.Values);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }
        public virtual async Task<TUpdateDTO> Update(Guid userId,TUpdateDTO DTO,CancellationToken cancellationToken = default)
        {
            TEntity entity;
            try
            {
                entity = _mapper.Map<TEntity>(DTO);
            }
            catch
            {
                throw new MappingException(this);
            }
            
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userId;
            _repository.Update(entity);
            return DTO;
        }
        public virtual async Task Delete(Guid userId,Guid entityId)
        {
            _repository.Delete(entityId,userId);
        }
    }
}
