using AutoMapper;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Logic.Exceptions;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class BaseDictionaryService<TEntity,TCreateDTO,TDTO>:IBaseDictionaryService<TEntity,TCreateDTO,TDTO> where TEntity: BaseDictionaryEntity where TCreateDTO: DictionaryDTO where TDTO:DictionaryDTO
    {
        protected IRepositoryWrapper _repositoryWrapper;
        protected IBaseDictionaryRepository<TEntity> _repository;
        protected IMapper _mapper;
        public BaseDictionaryService(IRepositoryWrapper repositoryWrapper,
            IBaseDictionaryRepository<TEntity> baseDictionaryRepository,
            IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _repository= baseDictionaryRepository;
            _mapper = mapper;
        }
        public async Task Create(Guid userid, TCreateDTO model)
        {
            TEntity entity;
            try
            {
                entity = _mapper.Map<TEntity>(model);
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            _repository.Create(entity, userid);
            _repositoryWrapper.Save();
            
        }
        public async Task Update(Guid userid, TCreateDTO model)
        {
             TEntity entity = _repository.GetById(model.Id).Result;
            try
            {
                entity = _mapper.Map<TEntity>(model);
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            _repository.Update(entity, userid);
            _repositoryWrapper.Save();
        }
        public async Task<PageModel<TDTO>> GetPage(FilterPagingModel model)
        {
            PageModel<TEntity> pagemodel = _repository.GetPage(_repository.GetAll().AsQueryable(), model.PageNumber, model.PageSize);
            PageModel<TDTO> result = new PageModel<TDTO>
            {
                CurrentPage = pagemodel.CurrentPage,
                TotalItems = pagemodel.TotalItems,
                TotalPages = pagemodel.TotalPages,
                ItemsOnPage = pagemodel.ItemsOnPage,
                Values = _mapper.Map<List<TDTO>>(pagemodel.Values)
            };
            return result;

        }
        public async Task Delete(Guid userid,Guid Id)
        {
            _repository.Delete(Id, userid);
        }
    }
}
