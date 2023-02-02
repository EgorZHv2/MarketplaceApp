using Data.DTO;
using Data.Entities;
using Data.Models;

namespace Logic.Interfaces
{
    public interface IBaseDictionaryService<TEntity, TCreateDTO, TDTO> where TEntity : BaseDictionaryEntity where TDTO : DictionaryDTO
    {
        public Task Create(Guid userid, TCreateDTO model, CancellationToken cancellationToken = default);

        public Task Update(Guid userid, TCreateDTO model, CancellationToken cancellationToken = default);

        public Task<PageModel<TDTO>> GetPage(FilterPagingModel model);

        public Task Delete(Guid userid, Guid entityId, CancellationToken cancellationToken = default);
    }
}