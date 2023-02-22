using AutoMapper;
using Data.DTO.Filters;
using Data.DTO;
using Data.DTO.Product;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;
using Data.Repositories.Repositories;
using Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Exceptions;

namespace Logic.Services
{
    public class ProductService:BaseService<ProductEntity,ProductDTO,CreateProductDTO,UpdateProductDTO,IProductRepository>,IProductService
    {
      private IUserRepository _userRepository;
        private ICategoryRepository _categoryRepository;
      public ProductService(IProductRepository repository,IMapper mapper,IUserRepository userRepository, ICategoryRepository categoryRepository):base(repository,mapper) 
      {
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
      }

      public async Task<PageModelDTO<ProductDTO>> GetPage(Guid userId,PaginationDTO pagingModel,ProductFilterDTO filter)
      {
            var result = new PageModelDTO<ProductDTO>();
            var user = await _userRepository.GetById(userId);
            var qeryable = _repository.GetFiltered(e => (e.IsActive ||user.Role == Data.Enums.Role.Admin));
            var pages = await _repository.GetPage(qeryable, pagingModel, filter);
            
            result = _mapper.Map<PageModelDTO<ProductDTO>>(pages);
            
            return result;
      }
        public override Task<Guid> Create(Guid userId, CreateProductDTO createDTO)
        {
            var category = _categoryRepository.GetById(createDTO.CategoryId);
            if(category == default)
            {
                throw new CategoryNotFoundException();
            }
            return base.Create(userId, createDTO);
        }
        public override Task<UpdateProductDTO> Update(Guid userId, UpdateProductDTO DTO)
        {
            var category = _categoryRepository.GetById(DTO.CategoryId);
            if(category == default)
            {
                throw new CategoryNotFoundException();
            }
            return base.Update(userId, DTO);
        }
    }
}
