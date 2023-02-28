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
            Guid userId,
            PaginationDTO pagingModel,
            ProductFilterDTO filter
        )
        {
            var result = new PageModelDTO<ProductDTO>();
            var user = await _userRepository.GetById(userId);
            var qeryable = _repository.GetFiltered(
                e => (e.IsActive || user.Role == Data.Enums.Role.Admin)
            );
            var pages = await _repository.GetPage(qeryable, pagingModel, filter);

            result = _mapper.Map<PageModelDTO<ProductDTO>>(pages);

            return result;
        }

        public override async Task<Guid> Create(Guid userId, CreateProductDTO createDTO)
        {
            var category = _categoryRepository.GetById(createDTO.CategoryId);
            if (category == default)
            {
                throw new CategoryNotFoundException();
            }
            var result = await base.Create(userId, createDTO);
            await _imageService.CreateManyImages(userId, createDTO.Photos, result);
            return result;
        }

        public override Task<UpdateProductDTO> Update(Guid userId, UpdateProductDTO DTO)
        {
            var category = _categoryRepository.GetById(DTO.CategoryId);
            if (category == default)
            {
                throw new CategoryNotFoundException();
            }
            return base.Update(userId, DTO);
        }

        public async Task AddProductsFromExcelFile(Guid userId, IFormFile excelFile) 
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
                            Tier1CategoryName = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                            Tier2CategoryName = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                            Tier3CategoryName = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                            Tier4CategoryName = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                            PartNumber = int.Parse(worksheet.Cells[row, 5].Value?.ToString().Trim()),
                            Name = worksheet.Cells[row, 6].Value.ToString()?.Trim(),
                            Description = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                            Weight = double.Parse(worksheet.Cells[row, 8].Value?.ToString().Trim()),
                            Width = double.Parse(worksheet.Cells[row, 9].Value?.ToString().Trim()),
                            Height = double.Parse(worksheet.Cells[row, 10].Value?.ToString().Trim()),
                            Depth = double.Parse(worksheet.Cells[row, 11].Value?.ToString().Trim()),
                            Country = worksheet.Cells[row, 12].Value?.ToString().Trim(),
                            Photos = (worksheet.Cells[row, 13].Value?.ToString().Trim().Replace(" ","")).Split(";").ToList()
                        });
                    }
                }
            }
            foreach(var item in list)
            {
                if (item.PartNumber == null)
                {
                    throw new RequiredImportPropertyException(nameof(item.PartNumber));
                }
                if (string.IsNullOrEmpty(item.Name))
                {
                    throw new RequiredImportPropertyException(nameof(item.Name));
                }
                if (string.IsNullOrEmpty(item.Description))
                {
                    throw new RequiredImportPropertyException(nameof(item.Description));
                }
                if (string.IsNullOrEmpty(item.Country))
                {
                    throw new RequiredImportPropertyException(nameof(item.Description));
                }
                if(item.Weight == null)
                {
                    throw new RequiredImportPropertyException(nameof(item.Weight));
                }
                 if(item.Width == null)
                {
                    throw new RequiredImportPropertyException(nameof(item.Width));
                }
                  if(item.Height == null)
                {
                    throw new RequiredImportPropertyException(nameof(item.Height));
                }
                   if(item.Depth == null)
                {
                    throw new RequiredImportPropertyException(nameof(item.Depth));
                }
                var productCategory = new Guid();
                if(string.IsNullOrEmpty(item.Tier1CategoryName))
                {
                     throw new CategoryNotFoundException();
                }
                var tier1category = await _categoryRepository.GetCategoryByName(item.Tier1CategoryName);
                if(tier1category == default)
                {
                   productCategory =  await _categoryService.Create(userId, new CreateCategoryDTO { Name = item.Tier1CategoryName, ParentCategoryId = null });
                }
                else
                {
                    productCategory = tier1category.Id;
                }
                if (!string.IsNullOrEmpty(item.Tier2CategoryName))
                {
                   var tier2category = await _categoryRepository.GetCategoryByName(item.Tier2CategoryName);
                   if(tier2category == default)
                   {
                        productCategory = await _categoryService.Create(userId, new CreateCategoryDTO { Name = item.Tier2CategoryName, ParentCategoryId = productCategory });
                   }
                   else
                   {
                        productCategory = tier2category.Id;
                   }

                }
                if (!string.IsNullOrEmpty(item.Tier3CategoryName))
                {
                   var tier3category = await _categoryRepository.GetCategoryByName(item.Tier3CategoryName);
                   if(tier3category == default)
                   {
                        productCategory = await _categoryService.Create(userId, new CreateCategoryDTO { Name = item.Tier3CategoryName, ParentCategoryId = productCategory });
                   }
                   else
                   {
                        productCategory = tier3category.Id;
                   }

                }
                if (!string.IsNullOrEmpty(item.Tier4CategoryName))
                {
                   var tier4category = await _categoryRepository.GetCategoryByName(item.Tier4CategoryName);
                   if(tier4category == default)
                   {
                        productCategory = await _categoryService.Create(userId, new CreateCategoryDTO { Name = item.Tier4CategoryName, ParentCategoryId = productCategory });
                   }
                   else
                   {
                        productCategory = tier4category.Id;
                   }

                }
                Country country = default(Country);
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
                    await _repository.Create(userId, product);

                    await _imageService.CreateManyImages(userId, item.Photos, product.Id);
                }
                else
                {
                    _mapper.Map(item, existingProduct);
                    existingProduct.CategoryId = productCategory;
                    existingProduct.Country = country;
                    await _repository.Update(userId, existingProduct);
                    await _imageService.CreateManyImages(userId,item.Photos,existingProduct.Id); 
                }
            }
            
        }
    }
}
