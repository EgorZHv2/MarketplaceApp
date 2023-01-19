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
    public class BaseDictionaryService<TEntity,TDTO>:IBaseDictionaryService<TEntity,TDTO> where TEntity: BaseDictionaryEntity where TDTO: DictionaryDTO
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IBaseDictionaryRepository<TEntity> _baseDictionaryRepository;
        private IMapper _mapper;
        public BaseDictionaryService(IRepositoryWrapper repositoryWrapper,
            IBaseDictionaryRepository<TEntity> baseDictionaryRepository,
            IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _baseDictionaryRepository= baseDictionaryRepository;
            _mapper = mapper;
        }
        public async Task Create(Guid userid, TDTO model)
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
            _baseDictionaryRepository.Create(entity, userid);
            _repositoryWrapper.Save();
            
        }
        public async Task Update(Guid userid, TDTO model)
        {
             TEntity entity = _baseDictionaryRepository.GetById(model.Id).Result;
            try
            {
                entity = _mapper.Map<TEntity>(model);
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            _baseDictionaryRepository.Update(entity, userid);
            _repositoryWrapper.Save();
        }
        public async Task<PageModel<TDTO>> GetPage(FilterPagingModel model)
        {
            PageModel<TEntity> pagemodel = _baseDictionaryRepository.GetPage(_baseDictionaryRepository.GetAll().AsQueryable(), model.PageNumber, model.PageSize);
            PageModel<TDTO> result = new PageModel<TDTO>
            {
                CurrentPage = pagemodel.CurrentPage,
                TotalItems = pagemodel.TotalItems,
                TotalPages = pagemodel.TotalPages,
                ItemsOnPage = pagemodel.ItemsOnPage,
                Values = _mapper.Map<IEnumerable<TDTO>>(pagemodel.Values)
            };
            return result;

        }
        public async Task Delete(Guid userid,Guid Id)
        {
            _baseDictionaryRepository.Delete(Id, userid);
        }
    }
}
