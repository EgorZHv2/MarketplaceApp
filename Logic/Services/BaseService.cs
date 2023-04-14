using AutoMapper;
using Data.DTO;
using Data.DTO.BaseDTOs;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class BaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>
        : IBaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>
        where TEntity : BaseEntity
        where TUpdateDTO : BaseUpdateDTO
        where TDTO : BaseOutputDTO
        where TCreateDTO : BaseCreateDTO
        where TRepository : IBaseRepository<TEntity>
    {
        protected TRepository _repository;
        protected IMapper _mapper;

        public BaseService(TRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<Guid> Create(TCreateDTO createDTO)
        {
            var entity = _mapper.Map<TEntity>(createDTO);
            Guid result = await _repository.Create(entity);
            return result;
        }

        public virtual async Task<TDTO> GetById(Guid Id)
        {
            var entity = await _repository.GetById(Id);
            if(entity == null)
            {
                throw new GenericEntityNotFoundException();
            }
            var result = _mapper.Map<TDTO>(entity);
            return result;
        }

        public virtual async Task<PageModelDTO<TDTO>> GetPage(PaginationDTO pagingModel)
        {
            PageModelDTO<TDTO> result = new PageModelDTO<TDTO>();
            var PageModel = await _repository.GetPage(pagingModel);
            result.CurrentPage = PageModel.CurrentPage;
            result.TotalPages = PageModel.TotalPages;
            result.ItemsOnPage = PageModel.ItemsOnPage;
            result.TotalItems = PageModel.TotalItems;
            result.Values = _mapper.Map<IEnumerable<TDTO>>(PageModel.Values);
            return result;
        }

        public virtual async Task Update(TUpdateDTO DTO)
        {
            TEntity? entity = await _repository.GetById(DTO.Id);
            if (entity == null)
            {
                throw new GenericEntityNotFoundException();
            }
            _mapper.Map(DTO, entity);
            await _repository.Update(entity);
            
        }

        public virtual async Task Delete(Guid entityId)
        {
            var entity = await _repository.GetById(entityId);
            if (entity != null)
            {
                await _repository.Delete(entity);
            }
            
        }

        public async Task<EntityActivityDTO> ChangeActivity(EntityActivityDTO model)
        {
            var entity = await _repository.GetById(model.Id);
            if (entity == null)
            {
                throw new GenericEntityNotFoundException();
            }
            entity.IsActive = model.IsActive;
            await _repository.Update(entity);
            return model;
        }
    }
}
