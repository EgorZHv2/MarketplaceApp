using AutoMapper;
using Data.DTO.Category;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class CategoryService : BaseDictionaryService<Category, CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, ICategoryRepository>, ICategoryService
    {
        public CategoryService(ICategoryRepository repository, IMapper mapper)
            : base(repository, mapper) { }

        public async Task<bool> CheckParentCategory(Guid categoryid, Guid parentid, CancellationToken cancellationToken = default)
        {
            if (categoryid == parentid)
            {
                return false;
            }
            var category = await _repository.GetById(categoryid);
            var parentcategory = await _repository.GetById(parentid);
            if (category == null)
            {
                throw new NotFoundException("Категория не найдена", "Category not found");
            }
            if (parentcategory == null)
            {
                throw new NotFoundException("Родительская категория не найдена", "Parent category not found");
            }

            var childrenCategories = await _repository.GetCategoriesByParentId(categoryid, cancellationToken);
            if (childrenCategories == null || childrenCategories.Count() == 0)
            {
                return true;
            }
            else
            {
                return await Check(childrenCategories, parentid, cancellationToken);
            }
        }

        public async Task<List<CategoryDTO>> GetCategoryTree(CancellationToken cancellationToken = default)
        {
            List<CategoryDTO> result = new List<CategoryDTO>();
            var list = await _repository.GetCategoriesWithoutParentId(cancellationToken);
            try
            {
                result = _mapper.Map<List<CategoryDTO>>(list);
            }
            catch
            {
                throw new MappingException(this);
            }
            result = await Fill(result, cancellationToken);
            return result;
        }

        private async Task<List<CategoryDTO>> Fill(List<CategoryDTO> level, CancellationToken cancellationToken = default)
        {
            foreach (var item in level)
            {
                item.Categories.AddRange(_mapper.Map<List<CategoryDTO>>(await _repository.GetCategoriesByParentId(item.Id)));
                await Fill(item.Categories, cancellationToken);
            }
            return level;
        }

        private async Task<bool> Check(IEnumerable<Category> list, Guid id, CancellationToken cancellationToken = default)
        {
            bool result = true;
            await CheckInner(list, id, cancellationToken);
            async Task CheckInner(IEnumerable<Category> list, Guid id, CancellationToken cancellationToken = default)
            {
                foreach (var item in list)
                {
                    if (item.Id == id)
                    {
                        result = false;
                        break;
                    }
                    item.Categories.AddRange(await _repository.GetCategoriesByParentId(item.Id, cancellationToken));
                    await CheckInner(item.Categories, id, cancellationToken);
                }
            }
            return result;
        }
    }
}