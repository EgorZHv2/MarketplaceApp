using Data.DTO.Filters;
using Data.DTO;
using Data.DTO.Product;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseGenericController<ProductEntity,ProductDTO,CreateProductDTO,UpdateProductDTO,IProductRepository,IProductService>
    {
        public ProductController(IProductService productService):base(productService) { }

         /// <summary>
        /// Получить продукт по айди
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public async Task<IActionResult> GetShopById(Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }
        /// <summary>
        /// Создать продукт
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Create(UserId, model);
            return Ok(result);
        }
        /// <summary>
        /// Изменить продукт
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Update(UserId, model);

            return Ok(result);
        }

        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public async Task<IActionResult> DeleteProduct( Guid id)
        {
            await _service.Delete(UserId, id);
            return Ok();
        }

       
        /// <summary>
        /// Получить страницу продуктов
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        [HttpGet("all-products")]
        public async Task<IActionResult> GetPage([FromQuery] PaginationDTO model, [FromQuery] ProductFilterDTO filter)
        {
            var result = await _service.GetPage(UserId, model,filter);
            return Ok(result);
        }

        [HttpPost("from-excel")]
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public async Task<IActionResult> UploadFromExcel([FromForm]UploadFileDTO dto)
        {
            await _service.AddProductsFromExcelFile(UserId, dto.File);
            return Ok();
        }

        [Authorize]
        [HttpGet("products-in-shops")]
        public async Task<IActionResult> GetProductsInShopsPage([FromQuery] PaginationDTO model, [FromQuery] ShopProductFilterDTO filter)
        {
            var result = await _service.GetProductsInShopsPage(UserId, model,filter);
            return Ok(result);
        }

    }
}
