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
    public class CategoryService : BaseDictionaryService<Category, CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, ICategoryRepository>, ICategoryService
    {
        private readonly IConfiguration _configuration;
        public CategoryService(ICategoryRepository repository, 
            IMapper mapper,
            IConfiguration configuration)
            : base(repository, mapper)
        {
            _configuration = configuration;
                }

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
        public override async Task<Guid> Create(Guid userId, CreateCategoryDTO createDTO, CancellationToken cancellationToken = default)
        {
            Category category = new Category();
            Category parent = new Category();
            if (createDTO.ParentCategoryId != null)
            {
                parent = await _repository.GetById((Guid)createDTO.ParentCategoryId, cancellationToken);
            }
            try
            {
                category = _mapper.Map<Category>(createDTO);
            }
            catch
            {
                throw new MappingException(this);
            }
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
                    throw new CategoryParentException($"Максимальный уровень категории {maxtier}", "Category tier over max tier");
                }
                else
                {
                    category.Tier = parent.Tier+1;
                }
            }
            Guid result = await _repository.Create(userId, category, cancellationToken);
            return result;
           
        }
        public async override Task<UpdateCategoryDTO> Update(Guid userId, UpdateCategoryDTO DTO, CancellationToken cancellationToken = default)
        {
            Category parent = new Category();
            if (DTO.ParentCategoryId != null)
            {
                parent = await _repository.GetById((Guid)DTO.ParentCategoryId, cancellationToken);     
                if (!await CheckParentCategory(DTO.Id, (Guid)DTO.ParentCategoryId,cancellationToken))
                {
                    throw new CategoryParentException("Ошибка при выборе родительской категории", "Parent category error");
                }
            }       
            Category category = new Category();
            try
            {
                category = _mapper.Map<Category>(DTO);
            }
            catch
            {
                throw new MappingException(this);
            }
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
                    throw new CategoryParentException($"Максимальный уровень категории {maxtier}", "Category tier over max tier");
                }
                else
                {
                    category.Tier = parent.Tier+1;
                }
            }
            await _repository.Update(userId, category, cancellationToken);
            return DTO;
            
        }
    }
}