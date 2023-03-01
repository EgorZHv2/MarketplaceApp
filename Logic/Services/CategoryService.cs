using AutoMapper;
using Data.DTO.Category;
using Data.Entities;
using Data.IRepositories;
using Data.Options;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.ComponentModel.Design;

namespace Logic.Services
{
    public class CategoryService
        : BaseDictionaryService<
            CategoryEntity,
            CategoryDTO,
            CreateCategoryDTO,
            UpdateCategoryDTO,
            ICategoryRepository
        >,
            ICategoryService
    {
        private readonly ApplicationOptions _options;

        public CategoryService(
            ICategoryRepository repository,
            IMapper mapper,
            IOptions<ApplicationOptions> options
        ) : base(repository, mapper)
        {
            _options = options.Value;
        }

        public async Task<bool> CheckParentCategory(Guid categoryId, Guid parentId)
        {
            if (categoryId == parentId)
            {
                return false;
            }
            var category = await _repository.GetById(categoryId);
            var parentCategory = await _repository.GetById(parentId);
            if (category == null)
            {
                throw new CategoryNotFoundException();
            }
            if (parentCategory == null)
            {
                throw new CategoryNotFoundException();
            }
            var childrenCategories = await _repository.GetCategoriesByParentId(categoryId);
            if (childrenCategories == null || !childrenCategories.Any())
            {
                return true;
            }
            else
            {
                return await Check(childrenCategories, parentId);
            }
        }

        public async Task<List<CategoryDTO>> GetCategoryTree()
        {
            var list = await _repository.GetCategoriesWithoutParentId();
            var result = _mapper.Map<List<CategoryDTO>>(list);
            result = await Fill(result);
            return result;
        }

        private async Task<List<CategoryDTO>> Fill(List<CategoryDTO> level)
        {
            foreach (var item in level)
            {
                item.Categories.AddRange(
                    _mapper.Map<List<CategoryDTO>>(
                        await _repository.GetCategoriesByParentId(item.Id)
                    )
                );
                await Fill(item.Categories);
            }
            return level;
        }

        private async Task<bool> Check(IEnumerable<CategoryEntity> list, Guid id)
        {
            bool result = true;
            await CheckInner(list, id);
            async Task CheckInner(IEnumerable<CategoryEntity> list, Guid id)
            {
                foreach (var item in list)
                {
                    if (item.Id == id)
                    {
                        result = false;
                        break;
                    }
                    item.Categories.AddRange(await _repository.GetCategoriesByParentId(item.Id));
                    await CheckInner(item.Categories, id);
                }
            }
            return result;
        }

        public override async Task<Guid> Create(Guid userId, CreateCategoryDTO createDTO)
        {
            var parent = new CategoryEntity();
            if (createDTO.ParentCategoryId != null)
            {
                parent = await _repository.GetById(createDTO.ParentCategoryId.Value);
            }
            var category = _mapper.Map<CategoryEntity>(createDTO);
            if (parent != null)
            {
                if (parent.Tier >= _options.MaxCategoryTier)
                {
                    throw new CategoryTierException(_options.MaxCategoryTier);
                }
                else
                {
                    category.Tier = parent.Tier + 1;
                }
            }
            var result = await _repository.Create(userId, category);
            return result;
        }

        public async override Task<UpdateCategoryDTO> Update(Guid userId, UpdateCategoryDTO DTO)
        {
            var parent = new CategoryEntity();
            if (DTO.ParentCategoryId != null)
            {
                parent = await _repository.GetById(DTO.ParentCategoryId.Value);
                if (!await CheckParentCategory(DTO.Id, DTO.ParentCategoryId.Value))
                {
                    throw new CategoryParentException();
                }
            }
            var category = _mapper.Map<CategoryEntity>(DTO);
            if (parent != null)
            {
                if (parent.Tier >= _options.MaxCategoryTier)
                {
                    throw new CategoryTierException(_options.MaxCategoryTier);
                }
                else
                {
                    category.Tier = parent.Tier + 1;
                }
            }
            await _repository.Update(userId, category);
            return DTO;
        }
    }
}
