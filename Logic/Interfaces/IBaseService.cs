using Data.DTO;
using Data.Models;

namespace Logic.Interfaces
{
    public interface IBaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO, TRepository>
    {
        public Task<Guid> Create(Guid userId, TCreateDTO createDTO, CancellationToken cancellationToken = default);

        public Task<List<TDTO>> GetAll(CancellationToken cancellationToken = default);

        public Task<TDTO> GetById(Guid Id, CancellationToken cancellationToken = default);

        public Task<PageModel<TDTO>> GetPage(FilterPagingModel pagingModel, CancellationToken cancellationToken = default);

        public Task<TUpdateDTO> Update(Guid userId, TUpdateDTO DTO, CancellationToken cancellationToken = default);

        public Task Delete(Guid userId, Guid entityId, CancellationToken cancellationToken = default);

        public Task<EntityActivityDTO> ChangeActivity(Guid userId, EntityActivityDTO model, CancellationToken cancellationToken = default);
    }
}