using Data.DTO.Filters;
using Data.DTO;
using Data.DTO.Product;
using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Logic.Interfaces
{
    public interface IProductService:IBaseService<ProductEntity,ProductDTO,CreateProductDTO,UpdateProductDTO,IProductRepository>
    {
        Task<PageModelDTO<ProductDTO>> GetPage(Guid userId, PaginationDTO pagingModel, ProductFilterDTO filter);
        Task AddProductsFromExcelFile(Guid userId, IFormFile excelFile);
        Task<PageModelDTO<ProductDTOWithPrice>> GetProductsInShopsPage(Guid userId,PaginationDTO pagingModel,ShopProductFilterDTO filter);
    }
}
