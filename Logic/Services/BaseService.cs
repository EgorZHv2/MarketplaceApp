using AutoMapper;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class BaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository> : IBaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>
        where TEntity : BaseEntity
        where TUpdateDTO : BaseUpdateDTO
        where TRepository : IBaseRepository<TEntity>
    {
        protected TRepository _repository;
        protected IMapper _mapper;

        public BaseService(TRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<Guid> Create(Guid userId, TCreateDTO createDTO, CancellationToken cancellationToken = default)
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

            Guid result = await _repository.Create(userId, entity, cancellationToken);
            return result;
        }

     
        public virtual async Task<TDTO> GetById(Guid Id, CancellationToken cancellationToken = default)
        {
            TDTO result;
            var entity = await _repository.GetById(Id, cancellationToken);
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

        public virtual async Task<PageModel<TDTO>> GetPage(FilterPagingModel pagingModel, CancellationToken cancellationToken = default)
        {
            PageModel<TDTO> result = new PageModel<TDTO>();
            var PageModel = _repository.GetPage(e=>e.IsActive, pagingModel.PageNumber, pagingModel.PageSize,cancellationToken).Result;
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

        public virtual async Task<TUpdateDTO> Update(Guid userId, TUpdateDTO DTO, CancellationToken cancellationToken = default)
        {
            TEntity entity = await _repository.GetById(DTO.Id);
            try
            {
                _mapper.Map(DTO, entity);
            }
            catch
            {
                throw new MappingException(this);
            }
            await _repository.Update(userId, entity);
            return DTO;
        }

        public virtual async Task Delete(Guid userId, Guid entityId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetById(entityId, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException("Объект не найден", "Object not found");
            }
            await _repository.Delete(userId, entity, cancellationToken);
        }

        public async Task<EntityActivityDTO> ChangeActivity(Guid userId, EntityActivityDTO model, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetById(model.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException("Объект не найден", "Object not found");
            }
            entity.IsActive = model.IsActive;
            await _repository.Update(userId, entity, cancellationToken);
            return model;
        }
    }
}