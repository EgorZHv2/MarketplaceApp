using AutoMapper;
using Data.DTO.Product;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ProductService:BaseService<ProductEntity,ProductDTO,CreateProductDTO,UpdateProductDTO,IProductRepository>,IProductService
    {
      public ProductService(IProductRepository repository,IMapper mapper):base(repository,mapper) { }
    }
}
