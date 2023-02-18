using Data.DTO;

namespace Logic.Interfaces
{
    public interface IBaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>
    {
        public Task<Guid> Create(Guid userId, TCreateDTO createDTO);

        public Task<TDTO> GetById(Guid Id);

        public Task<PageModelDTO<TDTO>> GetPage(PaginationDTO pagingModel);

        public Task<TUpdateDTO> Update(Guid userId, TUpdateDTO DTO);

        public Task Delete(Guid userId, Guid entityId);

        public Task<EntityActivityDTO> ChangeActivity(Guid userId, EntityActivityDTO model);
    }
}