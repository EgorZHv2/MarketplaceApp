using AutoMapper;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class CategoryService:BaseDictionaryService<Category,CreateCategoryDTO,CategoryDTO>,ICategoryService
    {
        public CategoryService(IRepositoryWrapper repositoryWrapper,ICategoryRepository repository,IMapper mapper)
            :base(repositoryWrapper, repository, mapper) { }
              /// <summary>
        /// Проверяет валидность айди родителя, не является ли он айди потомка.
        /// </summary>
        /// <returns> true если валидно, иначе false</returns>
        /// <exception cref="MappingException"></exception>
        public async Task<bool> CheckParentCategory(Guid categoryid,Guid parentid)
        {
            if(categoryid == parentid)
            {
                return false;
            }
            var category = await _repository.GetById(categoryid);   
            var parentcategory = await _repository.GetById(parentid);
            if(category == null)
            {
                throw new NotFoundException("Категория не найдена", "Category not found");
            }
            if(parentcategory == null)
            {
                throw new NotFoundException("Родительская категория не найдена", "Parent category not found");
            }
            List<Category> childrenCategories = new List<Category>();
            childrenCategories = _repository.GetAll().Where(e => e.ParentCategoryId == categoryid).ToList();
            if(childrenCategories == null)
            {
                return true;
            }
            else
            {
                return Check(childrenCategories,parentid);  
            }         
        }
  
        public async Task<List<CategoryDTO>> GetCategoryTree()
        {
            List<CategoryDTO> result = new List<CategoryDTO>();
            var list = _repository.GetAll().Where(e=>e.ParentCategoryId == null);
            try
            {
                result =_mapper.Map<List<CategoryDTO>>(list);
            }
            catch
            {
               throw new MappingException(this);
            }
            result = Fill(result);
            return result;
        }
       
        private List<CategoryDTO> Fill(List<CategoryDTO> level)
            {
               foreach(var item in level)
                {
               
                   item.Categories.AddRange(_mapper.Map<List<CategoryDTO>>(_repository.GetAll().Where(e=>e.ParentCategoryId == item.Id)));
                Fill(item.Categories);
                }
                return level;
            }

        private bool Check(List <Category> list, Guid id)
        {
            bool result = true;
            CheckInner(list, id);
            void CheckInner(List <Category> list, Guid id)
            {
                foreach (var item in list)
                {
                    if (item.Id == id)
                    {
                        result = false;
                        break;
                    }
                    item.Categories.AddRange(_repository.GetAll().Where(e => e.ParentCategoryId == item.Id));
                    CheckInner(item.Categories, id);
                }
              
            }
            return result;
        }
        
    }
}
