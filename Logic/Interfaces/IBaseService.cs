using Data.DTO;

namespace Logic.Interfaces
{
    public interface IBaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>
    {
        public Task<Guid> Create(TCreateDTO createDTO);

        public Task<TDTO> GetById(Guid Id);

        public Task<PageModelDTO<TDTO>> GetPage(PaginationDTO pagingModel);

        public Task Update(TUpdateDTO DTO);

        public Task Delete(Guid entityId);

        public Task<EntityActivityDTO> ChangeActivity(EntityActivityDTO model);
    }
}