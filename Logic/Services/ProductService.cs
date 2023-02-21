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

namespace Logic.Services
{
    public class ProductService:BaseService<ProductEntity,ProductDTO,CreateProductDTO,UpdateProductDTO,IProductRepository>,IProductService
    {
      private IUserRepository _userRepository;
      public ProductService(IProductRepository repository,IMapper mapper,IUserRepository userRepository):base(repository,mapper) 
      {
            _userRepository = userRepository;
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
    }
}
