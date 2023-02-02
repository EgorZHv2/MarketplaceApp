using AutoMapper;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class BaseDictionaryService<TEntity, TCreateDTO, TDTO> : IBaseDictionaryService<TEntity, TCreateDTO, TDTO> where TEntity : BaseDictionaryEntity where TCreateDTO : DictionaryDTO where TDTO : DictionaryDTO
    {
        protected IRepositoryWrapper _repositoryWrapper;
        protected IBaseDictionaryRepository<TEntity> _repository;
        protected IMapper _mapper;

        public BaseDictionaryService(IRepositoryWrapper repositoryWrapper,
            IBaseDictionaryRepository<TEntity> baseDictionaryRepository,
            IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _repository = baseDictionaryRepository;
            _mapper = mapper;
        }

        public async Task Create(Guid userid, TCreateDTO model, CancellationToken cancellationToken = default)
        {
            TEntity entity;
            try
            {
                entity = _mapper.Map<TEntity>(model);
            }
            catch
            {
                throw new MappingException(this);
            }
            await _repository.Create(userid, entity, cancellationToken);
        }

        public async Task Update(Guid userid, TCreateDTO model, CancellationToken cancellationToken = default)
        {
            TEntity entity = await _repository.GetById(model.Id, cancellationToken);
            try
            {
                entity = _mapper.Map(model, entity);
            }
            catch
            {
                throw new MappingException(this);
            }
            await _repository.Update(userid, entity, cancellationToken);
        }

        public async Task<PageModel<TDTO>> GetPage(FilterPagingModel model)
        {
            PageModel<TEntity> pagemodel = await _repository.GetPage(_repository.GetAll().AsQueryable(), model.PageNumber, model.PageSize);
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

        public async Task Delete(Guid userid, Guid entityId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetById(entityId, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException("Объект не найден", "Object not found");
            }
            await _repository.Delete(userid, entity, cancellationToken);
        }
    }
}