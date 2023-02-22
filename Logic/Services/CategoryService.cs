using AutoMapper;
using Data.DTO.Category;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.Design;

namespace Logic.Services
{
    public class CategoryService : BaseDictionaryService<CategoryEntity, CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, ICategoryRepository>, ICategoryService
    {
        private readonly IConfiguration _configuration;
        public CategoryService(ICategoryRepository repository, 
            IMapper mapper,
            IConfiguration configuration)
            : base(repository, mapper)
        {
            _configuration = configuration;
                }

        public async Task<bool> CheckParentCategory(Guid categoryId, Guid parentId)
        {
            if (categoryId == parentId)
            {
                return false;
            }
            var category = await _repository.GetById(categoryId);
            var parentcategory = await _repository.GetById(parentId);
            if (category == null)
            {
                throw new CategoryNotFoundException();
            }
            if (parentcategory == null)
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
            var result = new List<CategoryDTO>();
            var list = await _repository.GetCategoriesWithoutParentId();
            result = _mapper.Map<List<CategoryDTO>>(list);
            result = await Fill(result);
            return result;
        }

        private async Task<List<CategoryDTO>> Fill(List<CategoryDTO> level)
        {
            foreach (var item in level)
            {
                item.Categories.AddRange(_mapper.Map<List<CategoryDTO>>(await _repository.GetCategoriesByParentId(item.Id)));
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
            var category = new CategoryEntity();
            var parent = new CategoryEntity();
            if (createDTO.ParentCategoryId != null)
            {
                parent = await _repository.GetById(createDTO.ParentCategoryId.Value);
            }
            category = _mapper.Map<CategoryEntity>(createDTO);
            string? maxtierstr = _configuration.GetSection("MaxCategoryTier").Value;
            int maxtier;
            if(maxtierstr == "null" || string.IsNullOrEmpty(maxtierstr))
            {
                maxtier = int.MaxValue;
            }
            else
            {
                maxtier = Convert.ToInt32(maxtierstr);
            }
            if(parent != null)
            {
                if(parent.Tier >= maxtier)
                {
                    throw new CategoryTierException(maxtier);
                }
                else
                {
                    category.Tier = parent.Tier+1;
                }
            }
            var result = await _repository.Create(userId, category);
            return result;
           
        }
        public async override Task<UpdateCategoryDTO> Update(Guid userId, UpdateCategoryDTO DTO)
        {
            CategoryEntity parent = new CategoryEntity();
            if (DTO.ParentCategoryId != null)
            {
                parent = await _repository.GetById(DTO.ParentCategoryId.Value);     
                if (!await CheckParentCategory(DTO.Id, DTO.ParentCategoryId.Value))
                {
                    throw new CategoryParentException();
                }
            }       
            var category = new CategoryEntity();
            category = _mapper.Map<CategoryEntity>(DTO);
            string? maxtierstr = _configuration.GetSection("MaxCategoryTier").Value;
            int maxtier;
            if(maxtierstr == "null" || string.IsNullOrEmpty(maxtierstr))
            {
                maxtier = int.MaxValue;
            }
            else
            {
                maxtier = Convert.ToInt32(maxtierstr);
            }
            if(parent != null)
            {
                if(parent.Tier >= maxtier)
                {
                    throw new CategoryTierException(maxtier);
                }
                else
                {
                    category.Tier = parent.Tier+1;
                }
            }
            await _repository.Update(userId, category);
            return DTO;
            
        }
    }
}