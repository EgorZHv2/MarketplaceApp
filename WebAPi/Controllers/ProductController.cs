using Data.DTO.Product;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseGenericController<ProductEntity,ProductDTO,CreateProductDTO,UpdateProductDTO,IProductRepository,IProductService>
    {
        public ProductController(IProductService productService):base(productService) { }
    }
}
