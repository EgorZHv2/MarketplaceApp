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
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Data.DTO.Category;
using Data.Enums;

namespace Logic.Services
{
    public class ProductService
        : BaseService<
            ProductEntity,
            ProductDTO,
            CreateProductDTO,
            UpdateProductDTO,
            IProductRepository
        >,
            IProductService
    {
       
        private IUserRepository _userRepository;
        private ICategoryRepository _categoryRepository;
        private IImageService _imageService;
        private ICategoryService _categoryService;

        public ProductService(
            IProductRepository repository,
            IMapper mapper,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            IImageService imageService,
            ICategoryService categoryService

        ) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _imageService = imageService;
            _categoryService = categoryService;
        }

        public async Task<PageModelDTO<ProductDTO>> GetPage(
            PaginationDTO pagingModel,
            ProductFilterDTO filter
        )
        {
            
            var pages = await _repository.GetPage(pagingModel, filter);

            var result = _mapper.Map<PageModelDTO<ProductDTO>>(pages);

            return result;
        }
        public async Task<PageModelDTO<ProductDTOWithPrice>> GetProductsInShopsPage(
            PaginationDTO pagingModel,
            ShopProductFilterDTO filter
        )
        {
            
            var pages = await _repository.GetProductsInShopsPage(pagingModel, filter);
            
            var result = _mapper.Map<PageModelDTO<ProductDTOWithPrice>>(pages);

            return result;
        }
        public override async Task<Guid> Create(CreateProductDTO createDTO)
        {
            var category = _categoryRepository.GetById(createDTO.CategoryId);
            if (category == default)
            {
                throw new CategoryNotFoundException();
            }
            var result = await base.Create(createDTO);
            await _imageService.CreateManyImages(createDTO.Photos, result);
            return result;
        }

        public override async Task Update(UpdateProductDTO DTO)
        {
            var category = _categoryRepository.GetById(DTO.CategoryId);
            if (category == default)
            {
                throw new CategoryNotFoundException();
            }
            await base.Update(DTO);
        }

        public async Task AddProductsFromExcelFile(IFormFile excelFile) 
        {
            var list = new List<ExcelProductModel>();
            using(var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using(var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    for(int row = 2; row <= worksheet.Dimension.Rows;row++)
                    {
                        list.Add(new ExcelProductModel
                        {
                            Tier1CategoryName = worksheet.Cells[row, 1].Value?.ToString().Trim() ?? throw new CategoryNotFoundException(),
                            Tier2CategoryName = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                            Tier3CategoryName = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                            Tier4CategoryName = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                            PartNumber = int.Parse(worksheet.Cells[row, 5].Value?.ToString().Trim() ?? throw new RequiredImportPropertyException("Код")) ,
                            Name = worksheet.Cells[row, 6].Value.ToString().Trim() ?? throw new RequiredImportPropertyException("Название"),
                            Description = worksheet.Cells[row, 7].Value?.ToString().Trim() ?? throw new RequiredImportPropertyException("Описание"),
                            Weight = double.Parse(worksheet.Cells[row, 8].Value?.ToString().Trim()?? throw new RequiredImportPropertyException("Вес")),
                            Width = double.Parse(worksheet.Cells[row, 9].Value?.ToString().Trim()?? throw new RequiredImportPropertyException("Ширина")),
                            Height = double.Parse(worksheet.Cells[row, 10].Value?.ToString().Trim()?? throw new RequiredImportPropertyException("Высота")),
                            Depth = double.Parse(worksheet.Cells[row, 11].Value?.ToString().Trim()?? throw new RequiredImportPropertyException("Длинна")),
                            Country = worksheet.Cells[row, 12].Value?.ToString().Trim()?? throw new RequiredImportPropertyException("Страна"),
                            Photos = (worksheet.Cells[row, 13].Value?.ToString().Trim().Replace(" ","")).Split(";").ToList()
                        });
                    }
                }
            }
            foreach(var item in list)
            {
                
                var productCategory = new Guid();
                var tier1Category = await _categoryRepository.GetCategoryByName(item.Tier1CategoryName);
                if(tier1Category == default)
                {
                   productCategory =  await _categoryService.Create(new CreateCategoryDTO { Name = item.Tier1CategoryName, ParentCategoryId = null });
                }
                else
                {
                    productCategory = tier1Category.Id;
                }
                if (!string.IsNullOrEmpty(item.Tier2CategoryName))
                {
                   var tier2Category = await _categoryRepository.GetCategoryByName(item.Tier2CategoryName);
                   if(tier2Category == default)
                   {
                        productCategory = await _categoryService.Create(new CreateCategoryDTO { Name = item.Tier2CategoryName, ParentCategoryId = productCategory });
                   }
                   else
                   {
                        productCategory = tier2Category.Id;
                   }

                }
                if (!string.IsNullOrEmpty(item.Tier3CategoryName))
                {
                   var tier3Category = await _categoryRepository.GetCategoryByName(item.Tier3CategoryName);
                   if(tier3Category == default)
                   {
                        productCategory = await _categoryService.Create(new CreateCategoryDTO { Name = item.Tier3CategoryName, ParentCategoryId = productCategory });
                   }
                   else
                   {
                        productCategory = tier3Category.Id;
                   }

                }
                if (!string.IsNullOrEmpty(item.Tier4CategoryName))
                {
                   var tier4Category = await _categoryRepository.GetCategoryByName(item.Tier4CategoryName);
                   if(tier4Category == default)
                   {
                        productCategory = await _categoryService.Create(new CreateCategoryDTO { Name = item.Tier4CategoryName, ParentCategoryId = productCategory });
                   }
                   else
                   {
                        productCategory = tier4Category.Id;
                   }

                }
                var country = default(Country);
                try
                {
                   country = (Country)Enum.Parse(typeof(Country), item.Country);
                }
                catch
                {
                     throw new CountryNotFoundException();
                }
                var existingProduct = await _repository.GetByPartNumber(item.PartNumber.Value);
                if (existingProduct == default)
                {
                    var product = _mapper.Map<ProductEntity>(item);
                    product.CategoryId = productCategory;
                    product.Country = country;
                    await _repository.Create(product);
                    await _imageService.CreateManyImages(item.Photos, product.Id);
                }
                else
                {
                    _mapper.Map(item, existingProduct);
                    existingProduct.CategoryId = productCategory;
                    existingProduct.Country = country;
                    await _repository.Update(existingProduct);
                    await _imageService.CreateManyImages(item.Photos,existingProduct.Id); 
                }
            }
            
        }
    }
}
